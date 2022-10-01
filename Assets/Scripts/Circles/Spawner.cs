using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Circles
{
    internal class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Element m_elementPrefab;

        [Inject] 
        private ElementDrawer m_drawer;

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

        public Element SpawnAt(float angle) {
            var go = m_container.InstantiatePrefab(m_elementPrefab, Vector3.zero, Quaternion.identity, m_elementsContainer);
            var element = go.GetComponent<Element>();
            element.SetShape(m_config.OuterCircleRadius - m_elementPrefab.Height / 2f - m_spawnOffset, angle, m_elementAngularSize);
            Mediator.Publish(new ElementAdded(element));

            return element;
        }

        private void Start() {
            m_timer.SubscribeAt(1f, () => SpawnAt(RandomAngle()));
        }

        private float RandomAngle() => Random.Range(0, m_angleCount) * (360f / m_angleCount);
    }
}
