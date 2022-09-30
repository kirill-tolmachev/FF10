using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    internal static class VectorExtensions
    {
        public static Vector3 V3(this Vector2 vector, float z = 0f) => new(vector.x, vector.y, z);
    }
}
