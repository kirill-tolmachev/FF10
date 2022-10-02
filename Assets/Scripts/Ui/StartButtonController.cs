using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Infrastructure;
using Cinemachine;
using TMPro;
using UniMediator;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Ui
{
    internal class StartButtonController : MonoBehaviour, IMulticastMessageHandler<ReturnToStartScreen>
    {
        [SerializeField] 
        private float m_speed = 1f;
        
        [SerializeField]
        private float m_circleRadius;

        [SerializeField]
        private Element m_elementPrefab;
        
        [SerializeField]
        private Transform m_elementsContainer;

        [SerializeField] 
        private SpriteRenderer m_outerCircle;

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
        private int m_frameCount;
        private bool m_resetting;
        private bool m_autoStarting;

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
            
            if ((m_autoStarting || m_isPressing) && !m_resetting) {
                
                var speed = m_speed;

                if (m_autoStarting) {
                    m_text.text = "RESTART";
                    speed *= 3;
                }

                m_currentProgress += speed * Time.deltaTime;
                if (m_currentProgress > m_max + 20) {
                    m_autoStarting = false;
                    m_completed = true;
                    StartCoroutine(StartGame());
                }
            }
            else {
                m_currentProgress = Mathf.Max(0, m_currentProgress - m_speed * 2f * Time.deltaTime);
                if (m_resetting && Mathf.Approximately(m_currentProgress, 0f)) {
                    m_resetting = false;
                    m_autoStarting = true;
                }
            }

            if (!m_resetting) {
                string[] artifacts = { "ST4RT", "STAЯT", "FF10", "STRRT", "5TART", "5T4RT" };
                m_text.text = (m_frameCount++ % 1000 < 980) ? "START" : artifacts[Random.Range(0, artifacts.Length)];
            }

            foreach (var element in m_elements) {
                element.SetAngularSize(m_currentProgress);
            }
        }

        public IEnumerator StartGame() {
            Mediator.Publish(new CameraReset());
            m_menuCamera.Priority = 0;
            m_gameCamera.Priority = 50;

            yield return new WaitForSeconds(0.1f);
            m_text.text = "FF:10";
            yield return new WaitForSeconds(1.8f);
            m_outerCircle.enabled = true;

            Mediator.Publish(new GameStarted());
        }

        public void Handle(ReturnToStartScreen message) {
            StartCoroutine(EndGame(message.Lock));
        }

        private IEnumerator EndGame(bool lockedExternally) {
            m_gameCamera.Priority = 0;
            m_menuCamera.Priority = 50;

            m_outerCircle.enabled = false;
            m_completed = false;
            m_text.text = "F0:01";
            m_resetting = true;
            yield return new WaitForSeconds(2f);

            if (!lockedExternally)
                m_resetting = false;
        }

        public void SetText(string text) {
            m_text.text = text;
        }

        public void Unlock() {
            m_resetting = false;
        }
    }
}
