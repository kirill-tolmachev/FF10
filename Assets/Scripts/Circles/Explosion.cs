﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class Explosion : MonoBehaviour
    {
        [Inject]
        private Camera m_camera;

        private void Update() {
           // this.transform.LookAt(m_camera.transform);
        }
    }
}
