using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class EnemySpawned : IMulticastMessage, IRemovable
    {
        public Enemy Enemy { get; }

        public EnemySpawned(Enemy enemy) {
            Enemy = enemy;
        }
    }
}
