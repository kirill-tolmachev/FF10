using UniMediator;

namespace Assets.Scripts.Circles
{
    internal class ElementRemoved : IMulticastMessage
    {
        public Element Element { get; }

        public ElementRemoved(Element element)
        {
            Element = element;
        }
    }
}