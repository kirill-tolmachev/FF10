using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    [RequireComponent(typeof(LineRenderer))]
    internal class Element : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer m_lineRenderer;

        [Inject]
        private ElementDrawer m_drawer;

        [Inject]
        private Config m_config;

        public float Angle { get; private set; }
        public float Radius { get; private set; }
        public float AngularSize { get; private set; }

        public float Height => Mathf.Max(m_lineRenderer.startWidth, m_lineRenderer.endWidth);

        public void SetAngle(float angle) => SetShape(Radius, angle);
        public void SetShape(float radius) => SetShape(radius, Angle);

        public void SetShape(float radius, float angle) => SetShape(radius, angle, AngularSize);

        public void SetShape(float radius, float angle, float angularSize) {
            var shape = m_drawer.CreateShape(radius, angle, angularSize);

            m_lineRenderer.positionCount = shape.Length;
            m_lineRenderer.SetPositions(shape);

            Angle = angle;
            Radius = radius;
            AngularSize = angularSize;
        }

    }
}
