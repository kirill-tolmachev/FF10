using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class CameraRotator : MonoBehaviour
    {
        [SerializeField] 
        private float m_rotationSpeed;

        [SerializeField]
        private Transform m_target;
        
        private float m_radius;
        private float m_currentAngle;

        private void Start() {
            var position = m_target.position;
            
            float x = position.x;
            float y = position.y;
            
            m_radius = Mathf.Sqrt(x * x + y * y);
            m_currentAngle = -90f;
        }

        private void Update() {
            m_currentAngle += m_rotationSpeed * Time.deltaTime;
            m_target.position = Util.OnCircle(m_radius, m_currentAngle, m_target.position.z);
        }
    }
}
