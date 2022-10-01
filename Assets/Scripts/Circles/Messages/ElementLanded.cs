using UniMediator;

namespace Assets.Scripts.Circles
{
    internal class ElementLanded : IMulticastMessage
    {
        public Element Element { get; }

        public ElementLanded(Element element) {
            Element = element;
        }
    }
}