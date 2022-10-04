using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure;
using UniMediator;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Circles.Menu
{
    internal class StartGameState  : UiState
    {
        private float m_speed = 10f;
        private bool m_isHolding;
        private float m_currentProgress;

        [Inject] 
        private CameraManager m_cameraManager;

        public override void Load()
        {
            base.Load();
            DestroyInnerCircle();

            Mediator.Publish(new CameraReset());
            m_cameraManager.SwitchToMenuCamera();

            int elementCount = Config.InnerCircleElementCount;
            InnerCircle = new Element[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                float radius = Config.InnerCircleRadius;
                float angle = i * 360f / elementCount;
                InnerCircle[i] = Container.InstantiateElement(Config.InnerCircleElementPrefab, Config.ElementsContainer, radius, i * angle, 0f);
            }
        }

        private void DestroyInnerCircle() {
            for (int i = InnerCircle.Length - 1; i >= 0; i--) {
                Object.Destroy(InnerCircle[i]);
            }
        }

        public override void OnButtonHold() {
            base.OnButtonHold();

            m_isHolding = true;
        }

        public override void OnButtonRelease() {
            base.OnButtonRelease();

            m_isHolding = false;
        }

        public override void Update() {
            base.Update();

            int direction = m_isHolding ? 1 : -1;
            float maxProgress = 360f + 20f;
            m_currentProgress = Mathf.Clamp(m_currentProgress + direction * m_speed * Time.deltaTime, 0f, maxProgress);

            Accessor.SetProgress(Mathf.Clamp01(m_currentProgress / maxProgress));
            Accessor.SetText(GetCurrentText());

            if (Mathf.Approximately(m_currentProgress, maxProgress)) {
                Controller.SetState(new InGameState());
            }
        }

        private string GetCurrentText() => TryGetArtifactOverride(out var artifact) ? artifact : "START";

        private static readonly string[] Artifacts = { "ST4RT", "STAЯT", "FF10", "STRRT", "5TART", "5T4RT" };

        public static bool TryGetArtifactOverride(out string value)
        {
            value = Artifacts[Random.Range(0, Artifacts.Length)];
            return Time.frameCount % 1000 >= 980;
        }
    }
}