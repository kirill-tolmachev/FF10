using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class RoundIndexAdded : IMulticastMessage
    {
        public RoundIndex Index { get; }

        public RoundIndexAdded(RoundIndex index) {
            Index = index;
        }
    }
}
