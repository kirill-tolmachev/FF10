using System.Collections.Generic;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class EnemyDestroyed : IMulticastMessage
    {
        public Enemy Enemy { get; }

        public EnemyDestroyed(Enemy enemy) {
            Enemy = enemy;
        }
    }
}