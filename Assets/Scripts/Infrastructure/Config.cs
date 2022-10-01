using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    internal class Config : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer m_outerCircle;

        [SerializeField]
        private SpriteRenderer m_innerCircle;

        [SerializeField]
        private int m_elementPrecision;

        [SerializeField] 
        private int m_musicBpm;

        [SerializeField]
        public int MusicBpm => m_musicBpm;

        public float InnerCircleRadius => m_innerCircle.bounds.size.x / 2f;

        public float OuterCircleRadius => m_outerCircle.bounds.size.x / 2f;

        public int ElementPrecision => m_elementPrecision;
    }
}
