using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class EnemyColorSwitcher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        [SerializeField] private Gradient m_colors;

        [SerializeField] private float m_speed;

        void Start() {
            m_spriteRenderer.color = m_colors.Evaluate(0f);
        }

        void Update() {
            float t = Mathf.Repeat(Time.time * m_speed, 1f);
            m_spriteRenderer.color = m_colors.Evaluate(t);
        }
    }
}