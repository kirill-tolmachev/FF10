using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Infrastructure;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class LandedElementsController : GameSystem, 
        IMulticastMessageHandler<ElementLanded>, 
        IMulticastMessageHandler<GameObjectRemoved>,
        IMulticastMessageHandler<ElementRemoved>
    {
        [Inject]
        private Config m_config;

        [SerializeField] 
        private float m_horizontalScaleSpeed;

        private readonly List<Element> m_landedElements = new();

        public IReadOnlyList<Element> LandedElements => m_landedElements;

        public void AddElement(Element element) {
            m_landedElements.Add(element);
        }

        public void RemoveElement(Element element) {
            m_landedElements.Remove(element);
        }

        protected override void OnUpdate() {
            //for (int i = m_spawnedElements.Count - 1; i >= 0; i--) {
            //    for (int j = m_spawnedElements.Count - 1; j >= 0; j--) {
            //        var x = m_spawnedElements[i];
            //        var y = m_spawnedElements[j];

            //        if (!x || !y || x == y)
            //            continue;

            //        if (x.Overlaps(y)) {
                        
            //            Mediator.Publish(new ElementRemoved(y));
            //            Destroy(y);
                        
            //            x.SetLeftRight(Mathf.Min(x.Left(), y.Left()), Mathf.Max(x.Right(), y.Right()));
            //        }
            //    }
            //}

            foreach (var spawnedElement in m_landedElements) {
                spawnedElement.SetAngularSize(spawnedElement.AngularSize + m_horizontalScaleSpeed * Time.deltaTime);
            }
        }

        public void Handle(ElementLanded message) => AddElement(message.Element);

        public float GetTopRadiusAt(float angle) {
            float result = m_config.InnerCircleRadius;

            foreach (var element in m_landedElements) {
                var r = element.Radius + element.Height / 2f;
                if ((element.Contains(angle))
                    && r > result)
                    result = r;
            }

            return result;
        }

        public void Handle(GameObjectRemoved message) {
            if (message.Object is Element e)
                m_landedElements.Remove(e);
        }

        public void Handle(ElementRemoved message) {
            m_landedElements.Remove(message.Element);
        }
    }
}
