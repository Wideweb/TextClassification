using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFIDFExample
{
    class NeuralNetwork
    {
        private Input[] inputs;
        private Neuron[] neurons;

        private int numberOfSemanticClasses;
        private int significanceVectorLength;

        private readonly double educationSpeed = 0.7;
        private static int trainingRound = 1;

        public NeuralNetwork(int numberOfSemanticClasses, int significanceVectorLength)
        {
            this.numberOfSemanticClasses = numberOfSemanticClasses;
            this.significanceVectorLength = significanceVectorLength;

            inputs = new Input[significanceVectorLength];
            neurons = new Neuron[numberOfSemanticClasses];

            InitializeInputs();
            InitializeNeurons();
            CreateNetwork();
            SetZeroExcitations();
        }

        private void InitializeInputs()
        {
            for (var i = 0; i < significanceVectorLength; i++)
                inputs[i] = new Input(numberOfSemanticClasses);
        }

        private void InitializeNeurons()
        {
            for (var i = 0; i < numberOfSemanticClasses; i++)
                neurons[i] = new Neuron(significanceVectorLength);
        }

        private void CreateNetwork()
        {
            for (var j = 0; j < significanceVectorLength; j++)
            {
                for (var i = 0; i < numberOfSemanticClasses; i++)
                {
                    var link = new Link(neurons[i]);
                    inputs[j].OutgoingLinks[i] = link;
                    neurons[i].IncomingLinks[j] = link;
                }
            }
        }

        
        public void Study(double[] input, int correctAnswer)
        {
            var neuron = neurons[correctAnswer];
            var normalizedVector = NormalizeVector(input);

            for (var i = 0; i < neuron.IncomingLinks.Length; i++)
            {
                var incomingLink = neuron.IncomingLinks[i];
                incomingLink.Weight = incomingLink.Weight
                    + (educationSpeed / trainingRound) * (normalizedVector[i] - incomingLink.Weight);
            }

            trainingRound++;
        }

        private double[] NormalizeVector(double[] input)
        {
            double[] normalizedVector = new double[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                normalizedVector[i] = input[i] / vectorLength(input);
            }

            return normalizedVector;
        }

        
        public int Parse(double[] input)
        {
            var vector = new double[significanceVectorLength];
            for (var i = 0; i < numberOfSemanticClasses; i++)
            {
                for (var j = 0; j < significanceVectorLength; j++)
                {
                    vector[j] = inputs[j].OutgoingLinks[i].Weight;
                }

                neurons[i].Power = cosineSimilarity(vector, input);
            }
            int maxIndex = FindNeuronWithMaxExcitation();
            SetZeroExcitations();

            return maxIndex;
        }

        private int FindNeuronWithMaxExcitation()
        {
            var maxIndex = 0;
            for (var i = 1; i < neurons.Length; i++)
            {
                if (neurons[i].Power > neurons[maxIndex].Power)
                    maxIndex = i;
            }

            return maxIndex;
        }

        private void SetZeroExcitations()
        {
            foreach (var outputNeuron in neurons)
            {
                outputNeuron.Power = 0;
            }
        }

        
        private double cosineSimilarity(double[] vector1, double[] vector2)
        {
            return scalarProduct(vector1, vector2) / (vectorLength(vector1) * vectorLength(vector2));
        }

        private double vectorLength(double[] vector)
        {
            double vectorLength = 0;
            for (var i = 0; i < vector.Length; i++)
            {
                vectorLength += vector[i] * vector[i];
            }

            return Math.Sqrt(vectorLength);

        }

        private double scalarProduct(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length) throw new FormatException("scalarProduct method: invalid params");

            double product = 0;
            for (var i = 0; i < vector1.Length; i++)
            {
                product += vector1[i] * vector2[i];
            }

            return product;
        }
    }
}
