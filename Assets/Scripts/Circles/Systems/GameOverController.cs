using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using UniMediator;

namespace Assets.Scripts.Circles.Systems
{
    internal class GameOverController : GameSystem, IMulticastMessageHandler<GameOver>
    {
        public void Handle(GameOver message) {
            Mediator.Publish(new GamePaused());
        }
    }
}
