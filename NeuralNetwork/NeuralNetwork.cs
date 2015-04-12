using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class SingleLayerNeuralNetwork
    {
        private Input[] inputs;
        private Neuron[] neurons;

        private int numberOfSemanticClasses;
        private int significanceVectorLength;

        private readonly double educationSpeed = 0.7;
        private static int trainingRound = 1;

        public SingleLayerNeuralNetwork(string[] classes, string[] vocabulary)
        {
            this.numberOfSemanticClasses = classes.Count();
            this.significanceVectorLength = vocabulary.Count();

            inputs = new Input[significanceVectorLength];
            neurons = new Neuron[numberOfSemanticClasses];

            InitializeInputs(vocabulary);
            InitializeNeurons(classes);
            CreateNetwork();
            SetZeroExcitations();
        }

        private void InitializeInputs(string[] vocabulary)
        {
            for (var i = 0; i < significanceVectorLength; i++)
                inputs[i] = new Input(numberOfSemanticClasses, vocabulary[i]);
        }

        private void InitializeNeurons(string[] classes)
        {
            for (var i = 0; i < numberOfSemanticClasses; i++)
                neurons[i] = new Neuron(significanceVectorLength, classes[i]);
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

        
        public void Study(double[] input, string correctAnswer)
        {
            var neuron = neurons.Single(x => x.Value == correctAnswer);
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

        
        public string Parse(double[] input)
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

            return neurons[maxIndex].Value;
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
