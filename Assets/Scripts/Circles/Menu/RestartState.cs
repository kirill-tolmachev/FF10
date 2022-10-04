using System.Collections;
using Assets.Scripts.Circles.Systems;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles.Menu
{
    internal class RestartState : UiState
    {
        [Inject] 
        private CameraManager m_cameraManager;
        
        private bool m_resetting;

        private readonly float m_max = 380f;
        private float m_progress;
        private float m_speed;

        public override void Load()
        {
            base.Load();
            m_cameraManager.SwitchToMenuCamera();
            Accessor.SetText("F0:01");
            m_resetting = true;
            m_progress = m_max;
        }

        public override void Update() {
            if (m_resetting)
                OnResetting();
            else
                OnStarting();
        }

        private void OnResetting() {
            m_progress = Mathf.Max(0f, m_progress - m_speed * Time.deltaTime);
            Accessor.SetProgress(m_progress);

            if (Mathf.Approximately(0f, m_progress))
                m_resetting = false;
        }

        private void OnStarting() {
            m_progress = Mathf.Clamp(m_progress + m_speed * Time.deltaTime, 0f, m_max);
            Accessor.SetProgress(m_progress);

            if (Mathf.Approximately(m_progress, m_max))
                Controller.SetState(new InGameState());
        }
    }
}