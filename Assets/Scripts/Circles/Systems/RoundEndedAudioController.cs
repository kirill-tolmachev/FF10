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
    internal class RoundEndedAudioController : GameSystem, IMulticastMessageHandler<RoundEnded>
    {
        [SerializeField] 
        private AudioSource m_audio;

        public void Handle(RoundEnded message) {
            m_audio.Play();
        }
    }
}
