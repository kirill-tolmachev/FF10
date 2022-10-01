using UniMediator;

namespace Assets.Scripts.Circles
{
    internal class ElementUnlanded : IMulticastMessage
    {
        public Element Element { get; }

        public ElementUnlanded(Element element) {
            Element = element;
        }
    }
}