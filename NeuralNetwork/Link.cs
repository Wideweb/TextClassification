using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    class Link
    {
        public Neuron Neuron;
        public double Weight;

        public Link(Neuron neuron)
        {
            Neuron = neuron;
        }
    }
}
