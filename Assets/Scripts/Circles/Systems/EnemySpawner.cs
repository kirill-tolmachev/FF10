using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Circles.Systems
{
    internal class EnemySpawner : GameSystem, IMulticastMessageHandler<RoundEnded>
    {
        [SerializeField] 
        private Transform m_enemyContainer;

        [Inject] 
        private Timer m_timer;

        [Inject] 
        private Config m_config;

        [Inject] 
        private DiContainer m_container;

        private Enemy SpawnEnemy(float angle) {
            
            var go = m_container.InstantiatePrefab(m_config.EnemyPrefab, Vector3.zero, Quaternion.identity, m_enemyContainer);
            var enemy = go.GetComponent<Enemy>();
            enemy.SetPosition(m_config.OuterCircleRadius, angle);
            Mediator.Publish(new EnemySpawned(enemy));
            return enemy;
        }

        private void Start() {
            
        }

        public void Handle(RoundEnded message) {
            //if (message.Round == 1)
                SpawnEnemy(Random.Range(0, 360));
                //m_timer.SubscribeAt(m_config.Tick, () => SpawnEnemy(Random.Range(0, 360)));
        }
    }
}
