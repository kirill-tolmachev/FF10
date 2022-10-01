using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    internal class GameSystem : MonoBehaviour, IMulticastMessageHandler<GameStarted>, IMulticastMessageHandler<GamePaused>
    {
        protected bool IsRunning { get; private set; }

        public virtual void Handle(GameStarted message) {
            IsRunning = true;
        }

        public virtual void Handle(GamePaused message) {
            IsRunning = false;
        }

        private void Update() {
            if (!IsRunning)
                return;

            OnUpdate();
        }

        protected virtual void OnUpdate() {

        }
    }
}
