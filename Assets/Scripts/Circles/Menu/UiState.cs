using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Infrastructure;
using UniMediator;
using Zenject;

namespace Assets.Scripts.Circles.Menu
{
    internal class UiState : IDisposable
    {
        [Inject] 
        private Config m_config;

        [Inject] 
        private DiContainer m_container;

        [Inject]
        private UiController.UiAccessor m_accessor;

        [Inject] 
        private MessageRedirector m_messageRedirector;

        protected UiController.UiAccessor Accessor => m_accessor;

        protected UiController Controller => Accessor.Controller;

        protected Config Config => m_config;

        protected DiContainer Container => m_container;

        protected Element[] InnerCircle { get; set; }

        private List<object> m_messageHandles = new List<object>();

        public UiState() {
            Mediator.Publish(new AutoInjectMessage(this));
        }

        public virtual void Load() {
            Mediator.Publish(new UiStateLoadedMessage(this));
        }

        public virtual void Unload() {
            foreach (var handle in m_messageHandles) {
                m_messageRedirector.Unsubscribe(handle);
            }

            Mediator.Publish(new UiStateUnloadedMessage(this));
        }

        protected void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMulticastMessage {
            var handle = m_messageRedirector.Subscribe(callback);
            m_messageHandles.Add(handle);
        }

        public virtual void Update() {
        }

        public virtual void LateUpdate() {

        }

        public virtual void OnButtonHold() {
        }
        
        public virtual void OnButtonRelease()
        {

        }

        public void Dispose()
        {
            Unload();
            Accessor?.Dispose();
        }
    }
}
