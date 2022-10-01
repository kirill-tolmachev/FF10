using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class MainRotator : GameSystem, IMulticastMessageHandler<OnAlarmEnded>
    {
        [SerializeField]
        private float m_rotationSpeed;

        [SerializeField]
        private Transform m_target;

        [SerializeField] 
        private int m_switchDirectionAfter;

        private int m_totalRotations;

        protected override void OnUpdate() {
            Vector3 rotation = m_target.rotation.eulerAngles;
            rotation.z += m_rotationSpeed * Time.deltaTime;

            m_target.rotation = Quaternion.Euler(rotation); 
        }

        public void Handle(OnAlarmEnded message) {
            m_totalRotations++;
            if (m_totalRotations % m_switchDirectionAfter == 0)
                m_rotationSpeed *= -1;
        }
    }
}