using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Timing;
using UniMediator;

namespace Assets.Scripts.Circles.Systems
{
    internal class RoundCounter : GameSystem, IMulticastMessageHandler<OnAlarmStarted>
    {
        private int m_currentRound;

        public override void Handle(GameStarted message) {
            base.Handle(message);

            m_currentRound = 1;
        }

        public void Handle(OnAlarmStarted message) {
            Mediator.Publish(new RoundEnded(m_currentRound));
            m_currentRound++;
        }
    }
}
