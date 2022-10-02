using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class MainRotator : GameSystem, IMulticastMessageHandler<OnAlarmEnded>, IMulticastMessageHandler<CameraReset>
    {
        [SerializeField]
        private float m_rotationSpeed;

        [SerializeField]
        private Transform m_target;

        [SerializeField] 
        private int m_switchDirectionAfter;

        private int m_totalRotations;

        private Quaternion m_initialRotation;

        private void Start() {
            m_initialRotation = m_target.rotation;
        }

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

        public override void Handle(GameStarted message) {
            base.Handle(message);

            m_totalRotations = 0;
        }

        public void Handle(CameraReset message) {
            m_target.rotation = m_initialRotation;
        }
    }
}