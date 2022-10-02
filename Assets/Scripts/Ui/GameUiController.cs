using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Timing;
using DG.Tweening;
using TMPro;
using UniMediator;
using Zenject;

namespace Assets.Scripts.Ui
{
    internal class GameUiController : GameSystem, IMulticastMessageHandler<OnAlarmStarted>, IMulticastMessageHandler<OnAlarmEnded>
    {
        [Inject] private Timer m_timer;

        [SerializeField] private TMP_Text m_timeLeftLabel;

        private bool m_waiting;

        private int m_msPrecision = 2;

        private void LateUpdate() {
            if (!IsRunning)
                return;

            if (!m_waiting)
                m_timeLeftLabel.text = $"{m_timer.TimeLeft.Seconds:00}:{Hex(m_timer.TimeLeft.Milliseconds)}";
        }

        private string Hex(int ms) => string.Join("", ms.ToString("X").Take(m_msPrecision));

        public void Handle(OnAlarmStarted message) {
            m_timeLeftLabel.text = "FF:" + string.Join("", Enumerable.Repeat("F", m_msPrecision));
            SetLabelColor(Color.red);
            m_waiting = true;
        }

        public void Handle(OnAlarmEnded message) {
            SetLabelColor(Color.white);
            m_waiting = false;
        }

        public void Lock() {
            m_waiting = true;
        }

        public void Unlock() {
            m_waiting = false;
        }

        public void SetText(string text) {
            m_timeLeftLabel.text = text.ToUpperInvariant();
        }

        private void SetLabelColor(Color color) {
            m_timeLeftLabel.color = color;
            var oldColor = m_timeLeftLabel.fontSharedMaterial.GetColor(ShaderUtilities.ID_GlowColor);
            m_timeLeftLabel.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color(color.r, color.g, color.b, oldColor.a));
        }
    }
}
