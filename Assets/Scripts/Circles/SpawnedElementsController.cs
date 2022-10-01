using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class SpawnedElementsController : MonoBehaviour
    {
        private HashSet<Element> m_spawnedElements = new();

        public void AddElement(Element element) {
            m_spawnedElements.Add(element);
        }
    }
}
