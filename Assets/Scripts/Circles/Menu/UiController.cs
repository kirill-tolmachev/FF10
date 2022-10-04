using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Menu;
using Assets.Scripts.Circles.Messages;
using Cinemachine;
using TMPro;
using UniMediator;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles
{

    internal class UiController : MonoBehaviour
    {
        internal class UiAccessor : IDisposable
        {
            private readonly UiController m_controller;

            public UiController Controller => m_controller;

            public Element[] InnerCircle {
                get => m_controller.m_innerCircle;
                set => m_controller.m_innerCircle = value;
            }

            internal UiAccessor(UiController controller) {
                m_controller = controller;
                m_controller.AddAccessor(this);
            }

            private void InvalidateAccess() {
                if (!m_controller.CanAccess(this))
                    throw new InvalidOperationException("controller does not contain this accessor");
            }

            public void SetText(string text) {
                InvalidateAccess();
                m_controller.m_text.text = text;
            }

            public void SetTextColor(Color color) {
                InvalidateAccess();
                m_controller.m_text.color = color;
                var oldColor = m_controller.m_text.fontSharedMaterial.GetColor(ShaderUtilities.ID_GlowColor);
                m_controller.m_text.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color(color.r, color.g, color.b, oldColor.a));
            }

            public void SetProgress(float progress) {
                foreach (var element in InnerCircle)
                {
                    float angularSize = progress * 380f / InnerCircle.Length;
                    element.SetAngularSize(angularSize);
                }

            }

            public void Dispose() {
                m_controller.RemoveAccessor(this);
            }
        }

        private Element[] m_innerCircle;

        private void Start() => SetState(new StartGameState());

        [SerializeField] 
        private TMP_Text m_text;

        [Inject] 
        private DiContainer m_container;

        private readonly HashSet<UiAccessor> m_accessors = new();
        
        private UiState m_currentState;

        public UiAccessor Override() => new(this);

        private void AddAccessor(UiAccessor accessor) => m_accessors.Add(accessor);
        private void RemoveAccessor(UiAccessor accessor) => m_accessors.Remove(accessor);
        
        public void SetState(UiState state)
        {
            m_currentState?.Dispose();
            m_currentState = state;
            m_currentState.Load();
        }

        private bool CanAccess(UiAccessor accessor) {
            return m_accessors.Contains(accessor);
        }

        public bool IsLocked() => m_accessors.Any();

        public void Unlock() {
            m_accessors.Clear();
        }

        private void Update() {
            m_currentState?.Update();
        }

        private void LateUpdate() {
            m_currentState?.LateUpdate();
        }
    }
}
