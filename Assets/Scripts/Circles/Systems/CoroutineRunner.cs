using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    internal class CoroutineRunner : MonoBehaviour
    {
        public Coroutine Run(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}
