﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using Assets.Scripts.Infrastructure;
using Cinemachine;
using TMPro;
using UniMediator;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Ui
{
    internal class StartButtonController : MonoBehaviour
    {
        [SerializeField] 
        private float m_speed = 1f;
        
        [SerializeField]
        private float m_circleRadius;

        [SerializeField]
        private Element m_elementPrefab;
        
        [SerializeField]
        private Transform m_elementsContainer;

        private bool m_isPressing;

        private float m_currentProgress = 0f;

        private const float m_max = 180;

        [Inject]
        private DiContainer m_container;

        private Element[] m_elements;

        [SerializeField] private CinemachineVirtualCamera m_menuCamera;
        [SerializeField] private CinemachineVirtualCamera m_gameCamera;
        [SerializeField] private TMP_Text m_text;

        private bool m_completed;

        private void Start() {

            int elementCount = (int)(360 / m_max);

            m_elements = new Element[elementCount];
            for (int i = 0; i < elementCount; i++) {
                var go = m_container.InstantiatePrefab(m_elementPrefab, Vector3.zero, Quaternion.identity, m_elementsContainer);
                var element = go.GetComponent<Element>();
                element.SetShape(m_circleRadius, i * 360f / elementCount, 0);
                m_elements[i] = element;
            }
        }

        public void Process() {
            m_isPressing = true;
            
        }

        public void Cancel() {
            m_isPressing = false;
        }

        private void Update() {
            if (m_completed)
                return;

            if (m_isPressing) {
                m_currentProgress += m_speed * Time.deltaTime;
                if (m_currentProgress > m_max + 20) {
                    m_completed = true;
                    StartCoroutine(StartGame());
                }
                    
            }
            else {
                m_currentProgress = Mathf.Max(0, m_currentProgress - m_speed * 2f * Time.deltaTime);
            }

            foreach (var element in m_elements) {
                element.SetAngularSize(m_currentProgress);
            }
        }

        public IEnumerator StartGame() {
            m_menuCamera.Priority = 0;
            m_gameCamera.Priority = 50;

            yield return new WaitForSeconds(0.1f);
            m_text.text = "FF:10";
            yield return new WaitForSeconds(1.8f);
            

            Mediator.Publish(new GameStarted());
        }
    }
}
