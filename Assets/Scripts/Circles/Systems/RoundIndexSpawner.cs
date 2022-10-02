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

namespace Assets.Scripts.Circles.Systems
{
    internal class RoundIndexSpawner : GameSystem, IMulticastMessageHandler<RoundEnded>
    {
        [Inject] 
        private Config m_config;

        [Inject] 
        private DiContainer m_container;

        [SerializeField] 
        private Transform m_indexContainer;

        public void Handle(RoundEnded message) {
            int round = message.Round;
            float angle = -(round - 1) * (360f / (m_config.TotalRounds - 1));
            var pos = Util.OnCircle(m_config.InnerCircleRadius, angle);
            var go = m_container.InstantiatePrefab(m_config.RoundIndexPrefab, pos, Quaternion.Euler(new Vector3(0,0,angle - 90f)), m_indexContainer);
            var index = go.GetComponent<RoundIndex>();
            index.SetRound(round);

            Mediator.Publish(new RoundIndexAdded(index));
        }
    }
}
