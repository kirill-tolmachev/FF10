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

namespace Assets.Scripts.Circles
{
    internal class CameraRotator : GameSystem, IMulticastMessageHandler<CameraReset>
    {
        [SerializeField] 
        private float m_rotationSpeed;

        [SerializeField]
        private Transform m_target;

        private Vector3 m_initialPosition;

        private float m_radius;
        private float m_currentAngle;

        private void Start() {
            m_initialPosition = m_target.position;
            
            float x = m_initialPosition.x;
            float y = m_initialPosition.y;
            
            m_radius = Mathf.Sqrt(x * x + y * y);
            m_currentAngle = -90f;
        }

        protected override void OnUpdate() {
            m_currentAngle += m_rotationSpeed * Time.deltaTime;
            m_target.position = Util.OnCircle(m_radius, m_currentAngle, m_target.position.z);
        }

        public void Handle(CameraReset message) {
            m_target.position = m_initialPosition;
            m_currentAngle = -90f;
        }
    }
}
