using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    internal static class Util
    {
        public static Vector3 OnCircle(float radius, float angle) {
            return new Vector3(radius * Mathf.Cos(Mathf.Deg2Rad * angle), radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        }

        public static bool Contains(this Element element, float angle) {
            angle -= element.Angle;
            angle = Normalize(angle);

            float r = element.AngularSize / 2f;
            return angle < r && angle > -r;
        }

        private static float Normalize(float angle)
        {
            angle = angle % 360;
            if (angle >= 180)
            {
                return angle - 360;
            }
            if (angle < -180)
            {
                return angle + 360;
            }
            return angle;
        }
    }
}
