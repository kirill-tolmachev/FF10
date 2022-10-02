using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class RoundEnded : IMulticastMessage
    {
        public int Round { get; }

        public RoundEnded(int round) {
            Round = round;
        }
    }
}
