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
        public int trainingRound = 1;

        public Neuron(int incomingLinksCount)
        {
            IncomingLinks = new Link[incomingLinksCount];
            Power = 0;
        }
    }
}
