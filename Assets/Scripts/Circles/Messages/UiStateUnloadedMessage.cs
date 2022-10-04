using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Menu;
using UniMediator;
using Unity.VisualScripting;
using Zenject;

namespace Assets.Scripts.Circles.Messages
{
    internal class UiStateUnloadedMessage : IMulticastMessage
    {
        public UiState State { get; }

        public UiStateUnloadedMessage(UiState state) {
            State = state;
        }
    }
}
