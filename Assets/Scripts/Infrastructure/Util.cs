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

        public static bool Overlaps(this Element element, Element other) {
            return element.Contains(other.Angle) || 
                   element.Contains(other.Angle - other.AngularSize / 2f) || 
                   element.Contains(other.Angle + other.AngularSize / 2f);
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

        public static IList<ElementSize> Merge(IList<Element> intervals) {
            var result = intervals.OrderBy(x => x.Right() - x.Left()).Select(x => new ElementSize(x.Left(), x.Right())).ToList();

            int index = 0; // Stores index of last element

            // Traverse all input Intervals
            for (int i = 1; i < result.Count; i++)
            {
                // If this is not first Interval and overlaps
                // with the previous one
                if (result[index].Right >= result[i].Left)
                {
                    // Merge previous and current Intervals
                    result[index].Right = Mathf.Max(result[index].Right, result[i].Right);
                }
                else
                {
                    index++;
                    result[index] = result[i];
                }
            }

            return result;
        }

        public static float Right(this Element element) => NormalizeAngle(element.Angle + element.AngularSize / 2f);
        public static float Left(this Element element) => NormalizeAngle(element.Angle - element.AngularSize / 2f);

    }
}
