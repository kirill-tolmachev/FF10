using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    internal class AudioController : MonoBehaviour, IMulticastMessageHandler<GameStarted>, IMulticastMessageHandler<GamePaused>
    {
        [SerializeField] 
        private AudioSource m_audioSource;

        public void Handle(GameStarted message) {
            m_audioSource.Play();
        }

        public void Handle(GamePaused message) {
            m_audioSource.Stop();
        }
    }
}
