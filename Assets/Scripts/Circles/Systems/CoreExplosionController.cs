using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class CoreExplosionController : GameSystem, IMulticastMessageHandler<GameOver>
    {
        [Inject] 
        private DiContainer m_container;

        [SerializeField] 
        private Explosion m_explosion;

        public void Handle(GameOver message) {
            m_container.InstantiatePrefab(m_explosion, message.Killer.transform);
        }

    }
}
