using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Neuron
    {
        public Link[] IncomingLinks;
        public double Power { get; set; }
        public string Value;

        public Neuron(int incomingLinksCount, string value)
        {
            Value = value;
            IncomingLinks = new Link[incomingLinksCount];
            Power = 0;
        }
    }
}
