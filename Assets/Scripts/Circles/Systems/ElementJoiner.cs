using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class ElementJoiner : MonoBehaviour, IMulticastMessageHandler<ElementLanded>, IMulticastMessageHandler<GameObjectRemoved>
    {
        [Inject] 
        private Timer m_timer;

        private HashSet<Element> m_landedElements;

        public void Handle(ElementLanded message) {
            m_landedElements.Add(message.Element);
        }

        void Start() {

        }

        public void Handle(GameObjectRemoved message) {
            if (message.Object is Element e)
                m_landedElements.Remove(e);
        }
    }
}
