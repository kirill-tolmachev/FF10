using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniMediator;

namespace Assets.Scripts.Circles
{
    internal class ElementAdded : IMulticastMessage
    {
        public Element Element { get; }

        public ElementAdded(Element element) {
            Element = element;
        }
    }
}
