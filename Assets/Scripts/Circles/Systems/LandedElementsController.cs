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

            var elementToNewOwner = new Dictionary<Element, Element>();

            for (int i = m_landedElements.Count - 1; i >= 0; i--) {
                for (int j = m_landedElements.Count - 1; j >= 0; j--) {
                    var x = m_landedElements[i];
                    var y = m_landedElements[j];

                    if (!x || !y || x == y)
                        continue;

                    Element GetParent(Element candidate) => elementToNewOwner.TryGetValue(candidate, out var parent) ? parent : candidate;

                    if (x.OverlapsHorizontally(y)) {
                        if (GetParent(x) != y)
                            elementToNewOwner[y] = GetParent(x);
                    }
                }
            }

            foreach (var (element, owner) in elementToNewOwner) {
                var newAngle = Util.Midpoint(element.Radius, element.Angle, owner.Angle);
                owner.SetShape(owner.Radius, 
                    //(Util.NormalizeAngle(element.Angle) + Util.NormalizeAngle(owner.Angle)) / 2, 
                    newAngle,
                    element.AngularSize + owner.AngularSize);
                Mediator.Publish(new ElementRemoved(element));
                Destroy(element.gameObject);
            }

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
