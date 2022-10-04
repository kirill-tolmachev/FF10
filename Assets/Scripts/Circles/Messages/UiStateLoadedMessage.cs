using Assets.Scripts.Circles.Menu;
using UniMediator;

namespace Assets.Scripts.Circles.Messages
{
    internal class UiStateLoadedMessage : IMulticastMessage
    {
        public UiState State { get; }

        public UiStateLoadedMessage(UiState state) {
            State = state;
        }
    }
}