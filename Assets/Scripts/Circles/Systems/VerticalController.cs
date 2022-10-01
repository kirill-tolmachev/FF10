using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Infrastructure;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class VerticalController : GameSystem, IMulticastMessageHandler<ElementAdded>, IMulticastMessageHandler<ElementRemoved>
    {
        [SerializeField]
        private float m_fallSpeed;

        [Inject] 
        private LandedElementsController m_landedElementsController;

        private readonly List<Element> m_elements = new();

        private void LateUpdate() {
            if (!IsRunning)
                return;

            for (int i = m_elements.Count - 1; i >= 0; i--) {

                var element = m_elements[i];
                float targetRadius = m_landedElementsController.GetTopRadiusAt(element.Angle);
                element.SetRadius(Mathf.Clamp(element.Radius - m_fallSpeed * Time.deltaTime, targetRadius, element.Radius));
                
                if (Mathf.Approximately(element.Radius, targetRadius))
                {
                    m_elements.RemoveAt(i);
                    Mediator.Publish(new ElementLanded(element));
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
