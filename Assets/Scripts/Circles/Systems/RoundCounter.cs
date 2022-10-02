using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using UniMediator;
using Zenject;

namespace Assets.Scripts.Circles.Systems
{
    internal class RoundCounter : GameSystem, IMulticastMessageHandler<OnAlarmStarted>
    {
        [Inject]
        private Config m_config;

        private int m_currentRound;

        public override void Handle(GameStarted message) {
            base.Handle(message);

            m_currentRound = 1;
        }

        public void Handle(OnAlarmStarted message) {
            if (m_currentRound == m_config.TotalRounds) {
                Mediator.Publish(new GameWon());
                return;
            }

            Mediator.Publish(new RoundEnded(m_currentRound));
            m_currentRound++;
        }
    }
}
