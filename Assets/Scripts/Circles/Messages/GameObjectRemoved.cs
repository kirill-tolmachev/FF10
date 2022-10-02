using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class GameObjectRemoved : IMulticastMessage
    {
        public IRemovable Object { get; }

        public GameObjectRemoved(IRemovable o) {
            Object = o;
        }
    }
}
