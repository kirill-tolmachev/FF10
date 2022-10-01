using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Timing;
using DG.Tweening;
using TMPro;
using UniMediator;
using Zenject;

namespace Assets.Scripts.Ui
{
    internal class GameUiController : MonoBehaviour, IMulticastMessageHandler<OnAlarmStarted>, IMulticastMessageHandler<OnAlarmEnded>
    {
        [Inject] private Timer m_timer;

        [SerializeField] private TMP_Text m_timeLeftLabel;

        private bool m_waiting;

        private int m_msPrecision = 2;

        private void LateUpdate() {
            if (!m_waiting)
                m_timeLeftLabel.text = $"{m_timer.TimeLeft.Seconds:00}:{Hex(m_timer.TimeLeft.Milliseconds)}";
        }

        private string Hex(int ms) => string.Join("", ms.ToString("X").Take(2));

        public void Handle(OnAlarmStarted message) {
            m_timeLeftLabel.text = "FF:" + string.Join("", Enumerable.Repeat("F", m_msPrecision));
            m_timeLeftLabel.DOColor(Color.red, 0f);
            m_waiting = true;
        }

        public void Handle(OnAlarmEnded message)
        {
            m_timeLeftLabel.DOColor(Color.white, 0f);
            m_waiting = false;
        }
    }
}
