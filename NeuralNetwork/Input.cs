using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Input
    {
        public Link[] OutgoingLinks;
        public string Value;

        public Input(int outgoingLinksCount, string value)
        {
            Value = value;
            OutgoingLinks = new Link[outgoingLinksCount];
        }
    }
}
