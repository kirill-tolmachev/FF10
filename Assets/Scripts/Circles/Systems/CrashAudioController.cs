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
    internal class CrashAudioController : GameSystem, IMulticastMessageHandler<GameOver>
    {
        [SerializeField] 
        private AudioSource m_audio;

        public void Handle(GameOver message) {
            m_audio.Play();
        }
    }
}
