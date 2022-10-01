using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class VerticalController : MonoBehaviour, IMulticastMessageHandler<ElementAdded>, IMulticastMessageHandler<ElementRemoved>
    {
        [Inject] 
        private Config m_config;

        [SerializeField]
        private float m_fallSpeed;

        [Inject]
        private SpawnedElementsController m_spawnedElementsController;

        [Inject]
        private Scaler m_scaler;

        private readonly HashSet<Element> m_elements = new();

        private void LateUpdate() {
            foreach (var element in m_elements) {
                float targetRadius = m_config.InnerCircleRadius + element.Height / 2f;
                element.SetShape(Mathf.Lerp(element.Radius, targetRadius, m_fallSpeed * Time.deltaTime));

                if (Mathf.Approximately(element.Radius, targetRadius)) {
                    m_scaler.RemoveElement(element);
                    m_spawnedElementsController.AddElement(element);
                }     
            }
        }

        public void Handle(ElementAdded message) {
            m_elements.Add(message.Element);
        }

        public void Handle(ElementRemoved message) {
            m_elements.Remove(message.Element);
        }
    }
}
