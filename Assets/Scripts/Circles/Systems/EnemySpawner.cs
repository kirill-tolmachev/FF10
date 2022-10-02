using System;
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
    internal class EnemySpawner : GameSystem, IMulticastMessageHandler<RoundEnded>, IMulticastMessageHandler<GameOver>
    {
        [SerializeField] 
        private Transform m_enemyContainer;

        [Inject] 
        private Timer m_timer;

        [Inject] 
        private Config m_config;

        [Inject] 
        private DiContainer m_container;

        private int m_difficulty;

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

            m_difficulty++;
            int enemiesPerWave = m_difficulty;
            int waves = m_difficulty;
            float waveInterval = 8f / waves;

            m_currentSubscription = m_timer.SubscribeAt(waveInterval, () => StartCoroutine(SpawnWave(enemiesPerWave)));
        }

        private IEnumerator SpawnWave(int enemiesPerWave) {
            if (m_canSpawn)
                yield break;

            float intervalBetweenEnemies = 0.2f;
            var wait = new WaitForSeconds(intervalBetweenEnemies);

            for (int i = 0; i < enemiesPerWave; i++) {
                SpawnNextEnemy();
                yield return wait;
            }
        }

        private void SpawnNextEnemy() {
            int angleCount = 20;
            SpawnEnemy((2 * Random.Range(0, angleCount / 2) + 1) * (360f / angleCount));
        }

        public void Handle(GameOver message) {
            m_canSpawn = false;

            m_difficulty = 0;
        }

        public override void Handle(GameStarted message) {
            m_canSpawn = true;
        }
    }
}
