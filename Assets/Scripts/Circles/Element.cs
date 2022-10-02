using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    [RequireComponent(typeof(LineRenderer))]
    internal class Element : MonoBehaviour, IRemovable
    {
        [SerializeField]
        private LineRenderer m_lineRenderer;

        [Inject]
        private ElementDrawer m_drawer;

        public float Angle { get; private set; }
        public float Radius { get; private set; }
        public float AngularSize { get; private set; }

        public float Height => Mathf.Max(m_lineRenderer.startWidth, m_lineRenderer.endWidth);

        public void SetAngle(float angle) => SetShape(Radius, angle);
        public void SetRadius(float radius) => SetShape(radius, Angle);
        public void SetAngularSize(float angularSize) => SetShape(Radius, Angle, angularSize);

        public void SetLeftRight(float left, float right) => SetAngle((right - left) / 2f);

        public void SetShape(float radius, float angle) => SetShape(radius, angle, AngularSize);

        public void SetShape(float radius, float angle, float angularSize) {
            var shape = m_drawer.CreateShape(radius, angle, angularSize);

            m_lineRenderer.positionCount = shape.Length;
            m_lineRenderer.SetPositions(shape);

            Angle = angle;
            Radius = radius;
            AngularSize = angularSize;
        }

        private void LateUpdate() {
            if (IsRound())
                SetColor(Color.yellow);
        }

        public void SetColor(Color color) {
            m_lineRenderer.material.color = color;
        }

        private bool IsRound() => this.AngularSize >= 360f;

        private void OnDrawGizmos() {
            var pos = Util.OnCircle(Radius, Angle);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(pos, 0.01f);
        }
    }
}
