using System.Collections;
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
    internal class EnemySpawner : GameSystem, IMulticastMessageHandler<RoundEnded>, IMulticastMessageHandler<GameOver>, IMulticastMessageHandler<GameWon>
    {
        [SerializeField] 
        private Transform m_enemyContainer;

        [Inject] 
        private Timer m_timer;

        [Inject] 
        private Config m_config;

        [Inject] 
        private DiContainer m_container;

        [SerializeField] 
        private RoundInfo[] m_rounds;

        private TimerSubscription m_currentSubscription;
        private bool m_canSpawn;

        private Enemy SpawnEnemy(float angle) {
            
            var go = m_container.InstantiatePrefab(m_config.EnemyPrefab, Vector3.zero, Quaternion.identity, m_enemyContainer);
            var enemy = go.GetComponent<Enemy>();
            enemy.SetPosition(m_config.OuterCircleRadius, angle);
            Mediator.Publish(new EnemySpawned(enemy));
            return enemy;
        }
        
        public void Handle(RoundEnded message) {
            if (m_currentSubscription != null)
                m_timer.Unsubscribe(m_currentSubscription);

            if (m_rounds.Length > message.Round + 1) {
                var nextRoundInfo = m_rounds[message.Round + 1];
                m_currentSubscription = m_timer.SubscribeAt(nextRoundInfo.WaveInterval, () => SpawnWave(nextRoundInfo.EnemiesPerWave));
            }
        }

        private void SpawnWave(int enemies) {
            if (!m_canSpawn)
                return;

            for (int i = 0; i < enemies; i++) {
                SpawnNextEnemy();
            }
        }

        private void SpawnNextEnemy() {
            int angleCount = 20;
            SpawnEnemy((2 * Random.Range(0, angleCount / 2) + 1) * (360f / angleCount));
        }

        public void Handle(GameOver message) => Stop();

        public override void Handle(GameStarted message) {
            m_canSpawn = true;
        }

        public void Handle(GameWon message) => Stop();

        private void Stop() {
            m_canSpawn = false;
            if (m_currentSubscription != null)
                m_timer.Unsubscribe(m_currentSubscription);
        }
    }
}
