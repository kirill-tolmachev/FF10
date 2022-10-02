using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class ReturnToStartScreen : IMulticastMessage
    {
        public bool Lock { get; }

        public ReturnToStartScreen(bool @lock = false) {
            Lock = @lock;
        }
    }
}
