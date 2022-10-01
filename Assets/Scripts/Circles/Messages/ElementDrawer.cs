using System;
using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class ElementDrawer
    {
        private readonly IElementPrecisionProvider m_config;

        public ElementDrawer(IElementPrecisionProvider config) {
            m_config = config;
        }

        public Vector3[] CreateShape(float radius, float angle, float angularSize) {
            int precision = m_config.ElementPrecision * 2;

            float minAngle = angle - angularSize / 2f;
            float maxAngle = angle + angularSize / 2f;

            float step = (maxAngle - minAngle) / precision;

            var result = new Vector3[precision];
            for (int i = 0; i < precision; i++) {
                result[i] = Util.OnCircle(radius, minAngle + i * step);
            }

            return result;
        }
    }
}