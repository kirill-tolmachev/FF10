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
    internal class LandedElementsController : MonoBehaviour, IMulticastMessageHandler<ElementLanded>
    {
        [Inject]
        private Config m_config;

        private readonly HashSet<Element> m_spawnedElements = new();

        public void AddElement(Element element) {
            m_spawnedElements.Add(element);
        }

        public void RemoveElement(Element element) {
            m_spawnedElements.Remove(element);
        }

        public void Handle(ElementLanded message) => AddElement(message.Element);

        public float GetTopRadiusAt(float angle) {

            float result = m_config.InnerCircleRadius;

            foreach (var element in m_spawnedElements) {
                var r = element.Radius + element.Height;
                if ((element.Contains(angle) ||
                     element.Contains(angle - element.AngularSize / 2f) ||
                     element.Contains(angle + element.AngularSize / 2f))
                    && r > result)
                    result = r;
            }

            return result;
        }


    }
}
