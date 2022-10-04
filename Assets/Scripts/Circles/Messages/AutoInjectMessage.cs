using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class AutoInjectMessage : IMulticastMessage
    {
        public object Target { get; }

        public AutoInjectMessage(object target)
        {
            Target = target;
        }
    }
}
