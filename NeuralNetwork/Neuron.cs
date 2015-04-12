using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFIDFExample
{
    class Neuron
    {
        public Link[] IncomingLinks;
        public double Power { get; set; }

        public Neuron(int incomingLinksCount)
        {
            IncomingLinks = new Link[incomingLinksCount];
            Power = 0;
        }
    }
}
