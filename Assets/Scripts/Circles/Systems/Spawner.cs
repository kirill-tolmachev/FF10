using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Circles
{
    internal class Spawner : GameSystem, IMulticastMessageHandler<GameOver>, IMulticastMessageHandler<RoundEnded>, IMulticastMessageHandler<GameWon>
    {
        [Inject] 
        private Config m_config;

        [SerializeField]
        private float m_elementAngularSize;

        [SerializeField]
        private Transform m_elementsContainer;

        [SerializeField]
        private float m_spawnOffset;

        [SerializeField]
        private int m_angleCount = 20;

        [Inject] 
        private DiContainer m_container;

        [Inject] 
        private Timer m_timer;

        private TimerSubscription m_subscription;

        private Element SpawnAt(float angle) {

            var go = m_container.InstantiatePrefab(m_config.ElementPrefab, Vector3.zero, Quaternion.identity, m_elementsContainer);
            var element = go.GetComponent<Element>();
            element.SetShape(m_config.OuterCircleRadius - m_spawnOffset, angle, m_elementAngularSize);
            
            Mediator.Publish(new ElementAdded(element));

            return element;
        }

        private float RandomAngle() => 2 * Random.Range(0, m_angleCount / 2) * (360f / m_angleCount);
        
        public override void Handle(GameStarted message) {
            base.Handle(message);
            ResubscribeAtInterval(GetInterval(1));
        }

        public void Handle(RoundEnded message) {
            ResubscribeAtInterval(GetInterval(message.Round + 1));
        }

        private float GetInterval(int round) {
            if (round < 3)
                return 1.5f;

            if (round < 7)
                return 0.8f;

            return 0.5f;
        }

        private void ResubscribeAtInterval(float interval) {
            if (m_subscription != null)
                m_timer.Unsubscribe(m_subscription);

            m_subscription = m_timer.SubscribeAt(m_config.Tick * interval, () => SpawnAt(RandomAngle()));
        }


        public void Handle(GameOver message) => Stop();

        public void Handle(GameWon message) => Stop();

        private void Stop() {
            if (m_subscription != null)
                m_timer.Unsubscribe(m_subscription);
        }
    }
}
