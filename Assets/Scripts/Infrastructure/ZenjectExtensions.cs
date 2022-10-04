using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure
{
    internal static class ZenjectExtensions
    {
        public static Element InstantiateElement(this DiContainer container, Element prefab, Transform parent, float radius, float angle, float angularSize)
        {
            var element = container.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            element.SetShape(radius, angle, angularSize);

            return element;
        }

        public static TPrefab Instantiate<TPrefab>(this DiContainer container, TPrefab prefab, Vector3 position, Quaternion rotation, Transform parent) where TPrefab : Object {
            var go = container.InstantiatePrefab(prefab, position, rotation, parent);
            return go.GetComponent<TPrefab>();
        }
    }
}
