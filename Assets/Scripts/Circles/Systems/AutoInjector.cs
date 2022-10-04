using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles.Systems
{
    internal class AutoInjector : MonoBehaviour, IMulticastMessageHandler<AutoInjectMessage>
    {
        [Inject] 
        private DiContainer m_container;

        public void Handle(AutoInjectMessage message) => m_container.Inject(message.Target);
    }
}
