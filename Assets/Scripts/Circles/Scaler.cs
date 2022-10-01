using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using DG.Tweening;
using UniMediator;
using UnityEngine;
using Zenject;
using Sequence = DG.Tweening.Sequence;
using Timer = Assets.Scripts.Timing.Timer;

namespace Assets.Scripts.Circles
{
    internal class Scaler : MonoBehaviour, IMulticastMessageHandler<ElementAdded>, IMulticastMessageHandler<ElementRemoved>
    {
        private HashSet<Element> m_elements = new();

        [Inject]
        private Timer m_timer;

        [Inject] 
        private Config m_config;

        [SerializeField] 
        private float m_scale;

        private readonly Dictionary<Element, Sequence> m_sequences = new();

        public void AddElement(Element element) {
            m_elements.Add(element);
        }

        public void RemoveElement(Element element) {
            m_elements.Remove(element);
        }

        public void Handle(ElementAdded message) {
            var el = message.Element;

            AddElement(el);
            m_sequences[el] = CreateSequence(el);
        }

        public void Handle(ElementRemoved message) {
            RemoveElement(message.Element);
        }

        void Start() {
            m_timer.SubscribeAt(m_config.MusicBpm / 60f, () => Scale());
        }

        private void Scale() {
            Debug.Log("Scale!");
            foreach (var element in m_elements) {
                ScaleOne(element);
            }
        }

        private Sequence CreateSequence(Element element) {
            var sequence = DOTween.Sequence();
            sequence.SetAutoKill(false);
            sequence.SetRecyclable(true);

            var t = element.transform;

            float outDuration = 0.25f;
            float inDuration = 0.5f;

            sequence.Append(t.DOMove(Util.OnCircle(element.Radius * m_scale, element.Angle), outDuration));
            sequence.Append(t.DOMove(Vector3.zero, inDuration));
            
            return sequence;
        }

        private void ScaleOne(Element element) {
            var s = m_sequences[element];
            s.Rewind();
            s.Play();
        }
    }
}
