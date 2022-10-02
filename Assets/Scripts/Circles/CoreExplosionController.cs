using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using UniMediator;

namespace Assets.Scripts.Circles
{
    internal class CoreExplosionController : GameSystem, IMulticastMessageHandler<GameOver>
    {
        public void Handle(GameOver message) {
        
        }
    }
}
