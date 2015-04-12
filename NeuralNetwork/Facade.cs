using EntityDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NWTextClassificationDecorator
    {
        private string[] vocabulary;
        private SingleLayerNeuralNetwork nw;

        public NWTextClassificationDecorator(string[] classes, string[] vocabulary)
        {
            this.vocabulary = vocabulary;
            this.nw = new SingleLayerNeuralNetwork(classes, vocabulary);
        }

        public string Parse(string text)
        {
            string classification;

            return classification;
        }

        public void Study(List<Token> tokens, string correctAnswer)
        {
            double[] input = ComposeSignificanceVector(tokens);
            nw.Study(input, correctAnswer);
        }


        double[] ComposeSignificanceVector(List<Token> tokens)
        {
            double[] sv = new double[vocabulary.Length];

            for (var i = 0; i < vocabulary.Length; i++)
            {
                var token = tokens.FirstOrDefault(x => x.Value == vocabulary[i]);
                if (token == null)
                {
                    sv[i] = 0;
                }
                else
                {
                    sv[i] = token.TFIDF;
                }
            }

            return sv;
        }

    }
}
