using Assets.Scripts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Systems;
using UniMediator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Circles
{
    internal class HorizontalController : GameSystem, IMulticastMessageHandler<ElementLanded>, IMulticastMessageHandler<ElementUnlanded>
    {
        [SerializeField]
        private InputAction m_controls;

        [SerializeField] 
        private float m_speed;

        private readonly HashSet<Element> m_elements = new();

        public void Handle(ElementLanded message) => m_elements.Add(message.Element);

        public void Handle(ElementUnlanded message) => m_elements.Remove(message.Element);

        protected override void OnUpdate() {
            var movement = m_controls.ReadValue<float>();
            foreach (var element in m_elements) {
                element.SetAngle(element.Angle + movement * m_speed * Time.deltaTime);
            }
        }

        private void OnEnable()
        {
            m_controls.Enable();
        }

        private void OnDisable()
        {
            m_controls.Disable();
        }
    }
}
