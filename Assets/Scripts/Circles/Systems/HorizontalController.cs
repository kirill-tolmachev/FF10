using Assets.Scripts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using UniMediator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Circles
{
    internal class HorizontalController : GameSystem, IMulticastMessageHandler<ElementLanded>, IMulticastMessageHandler<ElementUnlanded>, IMulticastMessageHandler<GameObjectRemoved>, IMulticastMessageHandler<ElementRemoved>
    {
        [SerializeField]
        private InputAction m_controls;

        [SerializeField] 
        private InputAction m_shift;

        [SerializeField] 
        private float m_speed;


        [SerializeField]
        private float m_acceleration;

        private readonly HashSet<Element> m_elements = new();

        public void Handle(ElementLanded message) => m_elements.Add(message.Element);

        public void Handle(ElementUnlanded message) => m_elements.Remove(message.Element);

        protected override void OnUpdate() {
            var movement = m_controls.ReadValue<float>();
            var speed = m_speed;
            
            if (m_shift.ReadValue<float>() > 0f)
                speed *= m_acceleration;

            foreach (var element in m_elements.Where(e => e)) {
                element.SetAngle(element.Angle + movement * speed * Time.deltaTime);
            }
        }

        private void OnEnable() {
            m_controls.Enable();
            m_shift.Enable();
        }

        private void OnDisable() {
            m_controls.Disable();
            m_shift.Disable();
        }

        public void Handle(GameObjectRemoved message) {
            if (message.Object is Element e)
                m_elements.Remove(e);
        }

        public void Handle(ElementRemoved message) {
            m_elements.Remove(message.Element);
        }
    }
}
