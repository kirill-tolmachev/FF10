using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class EnemyIntersectsElement : IMulticastMessage
    {
        public Enemy Enemy { get; }

        public Element Element { get; }

        public EnemyIntersectsElement(Enemy enemy, Element element) {
            Enemy = enemy;
            Element = element;
        }
    }
}
