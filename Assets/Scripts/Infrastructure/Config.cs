using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    internal class Config : MonoBehaviour, IElementPrecisionProvider
    {
        [SerializeField] 
        private Element m_elementPrefab;

        [SerializeField]
        private Element m_innerCircleElementPrefab;

        [SerializeField]
        private Enemy m_enemyPrefab;

        [SerializeField]
        private RoundIndex m_roundIndexPrefab;

        [SerializeField]
        private SpriteRenderer m_outerCircle;

        [SerializeField]
        private SpriteRenderer m_innerCircle;

        [SerializeField]
        private int m_elementPrecision;

        [SerializeField] 
        private int m_musicBpm;

        [SerializeField] 
        private float m_minBlockSize;

        [SerializeField] 
        private int m_totalRounds;

        [SerializeField] 
        private float m_buttonSpeed = 1f;

        [SerializeField] 
        private Transform m_elementsContainer;

        [SerializeField] 
        private int m_innerCircleElementCount = 2;

        public int MusicBpm => m_musicBpm;

        public float Tick => MusicBpm / 60f;
        
        public float InnerCircleRadius => m_innerCircle.bounds.size.x / 2f + m_elementPrefab.Height * 1.5f;

        public float OuterCircleRadius => m_outerCircle.bounds.size.x / 2f - m_elementPrefab.Height / 2f;

        public int ElementPrecision => m_elementPrecision;
        public Element ElementPrefab => m_elementPrefab;
        public Element InnerCircleElementPrefab => m_innerCircleElementPrefab;
        public Enemy EnemyPrefab => m_enemyPrefab;
        public RoundIndex RoundIndexPrefab => m_roundIndexPrefab;

        public float MinBlockSize => m_minBlockSize;

        public int TotalRounds => m_totalRounds;

        public Transform ElementsContainer => m_elementsContainer;

        public int InnerCircleElementCount => m_innerCircleElementCount;
    }
}
