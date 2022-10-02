using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using ModestTree;
using Newtonsoft.Json.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    internal static class Util
    {
        public static Vector3 OnCircle(float radius, float angle, float z = 0f) {
            return new Vector3(radius * Mathf.Cos(Mathf.Deg2Rad * angle), radius * Mathf.Sin(Mathf.Deg2Rad * angle), z);
        }

        public static bool Contains(this Element element, float angle) {
            angle -= element.Angle;
            angle = NormalizeAngle(angle);

            float r = element.AngularSize / 2f;
            return angle < r && angle > -r;
        }

        public static bool OverlapsHorizontally(this Element element, Element other) {
            return Mathf.Approximately(element.Radius, other.Radius) && (
                element.Contains(other.Angle - other.AngularSize / 2f) ||
                element.Contains(other.Angle + other.AngularSize / 2f));
        }

        public static float NormalizeAngle(float angle)
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

        public class ElementSize
        {
            public float Left { get; set; }
            public float Right { get; set; }
            public ElementSize(float left, float right) {
                Left = left;
                Right = right;
            }
        }
        
        public static float Max(float x, float y, float z) => Mathf.Max(x, Mathf.Max(y, z));

        public static float Midpoint(float radius, float a, float b) {
            var p1 = OnCircle(radius, a);
            var p2 = OnCircle(radius, b);

            //var p0 = new Vector3(p1.x + p2.x, p1.y + p2.y) / 2f;
            //if (p0 == Vector3.zero)
            //    return Midpoint(radius, a, b + 0.001f);

            //var coords = p0 * radius / Mathf.Sqrt(p0.x * p0.x + p0.y * p0.y);
            //return Mathf.Atan2(coords.y, coords.x);

            float c = Mathf.Rad2Deg * Mathf.Atan2(p1.y + p2.y, p1.x + p2.x);
            return c;
            //float x_mid = radius * Mathf.Cos(c);
            //float y_mid = radius * Mathf.Sin(c);

            //return Mathf.Atan2(y_mid, x_mid);

        }
    }
}
