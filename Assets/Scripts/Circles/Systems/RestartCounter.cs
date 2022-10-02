using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Systems
{
    internal class RestartCounter : GameSystem
    {
        public int Count { get; set; }
        public override void Handle(GameStarted message) {
            base.Handle(message);
            Count++;
        }
    }
}
