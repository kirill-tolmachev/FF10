using System;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    [Serializable]
    public struct RoundInfo
    {
        [Range(0, 3)]
        public int EnemiesPerWave;

        [Range(1, 10)]
        public float WaveInterval;
        
    }
}