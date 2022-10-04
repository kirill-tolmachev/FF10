using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Timing;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    /// <summary>
    /// A hack around UniMediator being incapable of sending messages to non-MonoBehaviour instances
    /// </summary>
    internal class MessageRedirector : MonoBehaviour, 
        IMulticastMessageHandler<OnAlarmStarted>, 
        IMulticastMessageHandler<OnAlarmEnded>
    {
        private readonly Dictionary<object, (Type messageType, Action<IMulticastMessage> action)> m_redirects = new();
        private readonly Dictionary<Type, HashSet<object>> m_typeToHandles = new();
        
        public object Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMulticastMessage
        {
            var key = new object();

            var messageType = typeof(TMessage);
            m_redirects[key] = (messageType, m => callback((TMessage)m));

            if (!m_typeToHandles.TryGetValue(messageType, out var items))
                items = m_typeToHandles[messageType] = new HashSet<object>();

            items.Add(key);
            return key;
        }

        public void Unsubscribe(object handle)
        {
            if (!m_redirects.TryGetValue(handle, out var redirect))
                return;

            m_typeToHandles[redirect.messageType].Remove(handle);
            m_redirects.Remove(handle);
        }

        private void Redirect(IMulticastMessage message) {
            foreach (var handle in m_typeToHandles[message.GetType()]) {
                if (!m_redirects.TryGetValue(handle, out var redirect))
                    continue;
                
                redirect.action(message);
            }
        }

        public void Handle(OnAlarmStarted message) => Redirect(message);

        public void Handle(OnAlarmEnded message) => Redirect(message);
    }
}
