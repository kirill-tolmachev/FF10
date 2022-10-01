using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class Rotator : MonoBehaviour
    {
        [SerializeField]
        private float m_rotationSpeed;

        [SerializeField]
        private Transform m_target;
        
        private void Update() {
            Vector3 rotation = m_target.rotation.eulerAngles;
            rotation.z += m_rotationSpeed * Time.deltaTime;

            m_target.rotation = Quaternion.Euler(rotation);
            //m_currentAngle += m_rotationSpeed * Time.deltaTime;
            //m_target.position = Util.OnCircle(m_radius, m_currentAngle, m_target.position.z);
        }
    }
}