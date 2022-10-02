using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Infrastructure;
using UniMediator;
using Zenject;

namespace Assets.Scripts.Circles.Systems
{
    internal class ElementContactController : GameSystem, IMulticastMessageHandler<EnemyIntersectsElement>
    {
        [Inject] 
        private Config m_config;

        public void Handle(EnemyIntersectsElement message) {
            DestroyEnemy(message.Enemy);

            var newSize = message.Element.AngularSize;
            if (newSize < m_config.MinBlockSize)
                DestroyElement(message.Element);
            else
                ShrinkElement(message.Element);
        }

        private void DestroyEnemy(Enemy enemy) {
            Mediator.Publish(new EnemyDestroyed(enemy));
            Destroy(enemy.gameObject);
        }

        private void DestroyElement(Element element) {
            Mediator.Publish(new ElementRemoved(element));
            Destroy(element.gameObject);
        }

        private void ShrinkElement(Element element) {
            element.SetAngularSize(element.AngularSize / 2f);
        }
    }
}