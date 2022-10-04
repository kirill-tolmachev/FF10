using System;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Timing;
using System.Linq;
using TMPro;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles.Menu
{
    internal class InGameState : UiState
    {
        [Inject] 
        private Timer m_timer;

        private bool m_waiting;

        private const int MsPrecision = 2;

        public override void Load()
        {
            base.Load();

            Accessor.SetProgress(1f);
            Subscribe<OnAlarmStarted>(OnAlarmStarted);
            Subscribe<OnAlarmEnded>(OnAlarmEnded);
            Subscribe<GameWon>(OnGameWon);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (!m_waiting)
                Accessor.SetText($"{m_timer.TimeLeft.Seconds:00}:{Hex(m_timer.TimeLeft.Milliseconds)}");
        }

        private string Hex(int ms) => string.Join("", ms.ToString("X").Take(MsPrecision));

        private void OnAlarmStarted(OnAlarmStarted message) {
            Accessor.SetText("FF:" + string.Join("", Enumerable.Repeat("F", MsPrecision)));
            Accessor.SetTextColor(Color.red);
            m_waiting = true;
        }

        private void OnAlarmEnded(OnAlarmEnded message) {
            Accessor.SetTextColor(Color.white);
            m_waiting = false;
        }

        private void OnGameWon(GameWon message) {
            Controller.SetState(new CreditsState());
        }
    }
}