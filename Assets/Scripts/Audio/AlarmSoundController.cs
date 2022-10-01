using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    internal class AlarmSoundController : MonoBehaviour, IMulticastMessageHandler<OnAlarmStarted>
    {
        private AudioSource m_audio;

        private void Start() {
            m_audio = GetComponent<AudioSource>();
        }

        public void Handle(OnAlarmStarted message) {
            m_audio.Play();
        }
    }
}
