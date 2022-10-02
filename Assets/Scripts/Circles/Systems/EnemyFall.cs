﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Infrastructure;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles.Systems
{
    internal class EnemyFall : GameSystem, IMulticastMessageHandler<EnemySpawned>, IMulticastMessageHandler<EnemyDestroyed>, IMulticastMessageHandler<GameObjectRemoved>
    {
        [SerializeField] 
        private float m_speed;

        [Inject] 
        private LandedElementsController m_landedElementsController;

        [Inject] 
        private Config m_config;

        private readonly HashSet<Enemy> m_enemies = new();

        public void Handle(EnemySpawned message) {
            m_enemies.Add(message.Enemy);
        }

        public void Handle(EnemyDestroyed message) {
            m_enemies.Remove(message.Enemy);
        }

        protected override void OnUpdate() {
            var intersections = new List<(Enemy, Element)>();

            foreach (var enemy in m_enemies) {
                float target = m_landedElementsController.GetTopRadiusAt(enemy.Angle);
                enemy.SetPosition(Mathf.Max(enemy.Radius - Time.deltaTime * m_speed, target), enemy.Angle);

                if (EnemyIntersectsCore(enemy)) {
                    Mediator.Publish(new GameOver());
                    break;
                }

                if (EnemyIntersectsBlock(enemy, out var element)) {
                    intersections.Add((enemy, element));
                }
            }

            foreach (var (enemy, element) in intersections) {
                Mediator.Publish(new EnemyIntersectsElement(enemy, element));
            }
        }

        private bool EnemyIntersectsCore(Enemy enemy) {
            return enemy.Radius <= m_config.InnerCircleRadius;
        }

        private bool EnemyIntersectsBlock(Enemy enemy, out Element element) {
            element = m_landedElementsController.LandedElements.FirstOrDefault(x => x.Contains(enemy.Angle) && (x.Radius + x.Height/ 2f + 0.01f) >= enemy.Radius);
            return element != null;
        }

        public void Handle(GameObjectRemoved message) {
            if (message.Object is Enemy e)
                m_enemies.Remove(e);
        }
    }
}
