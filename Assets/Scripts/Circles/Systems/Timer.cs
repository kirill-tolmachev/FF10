using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using Assets.Scripts.Circles.Systems;
using UniMediator;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Timing
{
    class TimerSubscription
    {
        public float Interval { get; }
        public float Last { get; set; }

        public Action Callback { get; }

        public TimerSubscription(float interval, Action callback) {
            Interval = interval;
            Callback = callback;
        }
    }

    internal class Timer : GameSystem
    {
        [SerializeField]
        private float m_secondsInterval;

        [FormerlySerializedAs("m_waitInterval")] 
        
        [SerializeField]
        private float m_alarmInterval;

        private HashSet<TimerSubscription> m_subscriptions = new();

        public TimeSpan TimeLeft { get; private set; }

        private float m_last;

        private WaitForSeconds m_wait;

        private Coroutine m_runCoroutine;

        private void Start() {
         
        }

        private float Now => Time.time;

        private IEnumerator Run() {

            while (true) {
                yield return null;
                if (!IsRunning)
                    continue;
                
                float GetTimeLeft(float interval, float last) => interval - (Now - last);

                foreach (var subscription in m_subscriptions) {
                    var timeLeft = GetTimeLeft(subscription.Interval, subscription.Last);
                    if (timeLeft < 0f) {
                        subscription.Callback();
                        subscription.Last = Now;
                    }
                }

                TimeLeft = TimeSpan.FromSeconds(GetTimeLeft(m_secondsInterval, m_last));

                if (TimeLeft < TimeSpan.Zero) {
                    Mediator.Publish(new OnAlarmStarted());
                    yield return m_wait;
                    Mediator.Publish(new OnAlarmEnded());
                    m_last = Now;
                }
            }
        }

        public TimerSubscription SubscribeAt(float interval, Action callback) {
            var subscription = new TimerSubscription(interval, callback);
            m_subscriptions.Add(subscription);

            return subscription;
        }

        public void Unsubscribe(TimerSubscription subscription) {
            m_subscriptions.Remove(subscription);
        }

        public override void Handle(GameStarted message) {
            
            base.Handle(message);

            if (m_runCoroutine != null)
                StopCoroutine(m_runCoroutine);

            m_last = Time.time;
            m_wait = new WaitForSeconds(m_alarmInterval);
            m_runCoroutine = StartCoroutine(Run());
        }
    }
}
