using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    internal class CleanupController : GameSystem, 
        IMulticastMessageHandler<EnemySpawned>, 
        IMulticastMessageHandler<ElementAdded>, 
        IMulticastMessageHandler<RoundIndexAdded>,
        IMulticastMessageHandler<ReturnToStartScreen>
    {
        private readonly List<IRemovable> m_cache = new();

        public void Handle(EnemySpawned message) {
            m_cache.Add(message.Enemy);
        }

        public void Handle(ElementAdded message) {
            m_cache.Add(message.Element);
        }
        public void Handle(RoundIndexAdded message) {
            m_cache.Add(message.Index);
        }

        public void Handle(ReturnToStartScreen message) {
            for (int i = m_cache.Count - 1; i >= 0; i--) {
                var go = m_cache[i];
                var mb = go as MonoBehaviour;
                if (mb) {
                    Mediator.Publish(new GameObjectRemoved(go));
                    Destroy(mb.gameObject);
                }
                    
            }

            m_cache.Clear();
        }

    }
}
