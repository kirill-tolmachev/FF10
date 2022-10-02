using Assets.Scripts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class Enemy : MonoBehaviour, IRemovable
    {
        public float Radius { get; private set; }
        public float Angle { get; private set; }

        public void SetPosition(float radius, float angle) {
            var position = Util.OnCircle(radius, angle);
            var rotation = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(position.y, position.x) - 45);

            transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));

            Radius = radius;
            Angle = angle;
        }

        private void OnDrawGizmos()
        {
            var pos = Util.OnCircle(Radius, Angle);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pos, 0.01f);
        }
    }
}
