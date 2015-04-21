using EnglishStemmer;
using EntityDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class TextClassifyingNeuralNetwork
    {
        private string[] vocabulary;
        private string[] classes;
        private SingleLayerNeuralNetwork nw;

        public TextClassifyingNeuralNetwork(string[] classes, string[] vocabulary)
        {
            this.classes = classes;
            this.vocabulary = vocabulary;
            this.nw = new SingleLayerNeuralNetwork(classes, vocabulary);
        }

        public string Parse(string text)
        {
            string[] tokens = Transform(text);
            int[] input = new int[vocabulary.Length];

            for (var i = 0; i < vocabulary.Length; i++)
            {
                input[i] = tokens.Contains(vocabulary[i]) ? 1 : 0;
            }

            return classes[nw.Parse(input)];
        }

        public void Study(List<Token> tokens, string correctAnswer)
        {
            double[] input = ComposeSignificantVector(tokens);
            
            int neuronNumber = classes
                .Select((x, i) => i)
                .First(i => classes[i] == correctAnswer);

            nw.Study(input, neuronNumber);
        }

        double[] ComposeSignificantVector(List<Token> tokens)
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

        private string[] Transform(string text)
        {
            var output = new List<string>();
            string[] tokens = Tokenize(text);

            foreach (var token in tokens)
            {
                if (token == String.Empty) continue;

                string stripped = Regex.Replace(token, "[^a-zA-Z0-9]", "");

                if (!StopWords.stopWordsList.Contains(stripped.ToLower()))
                {
                    var english = new EnglishWord(stripped);
                    output.Add(english.Stem);
                }
            }

            return output.ToArray();
        }

        private static string[] Tokenize(string text)
        {
            text = Regex.Replace(text, "<[^<>]+>", "");
            text = Regex.Replace(text, "[0-9]+", "number");
            text = Regex.Replace(text, @"(http|https)://[^\s]*", "httpaddr");
            text = Regex.Replace(text, @"[^\s]+@[^\s]+", "emailaddr");
            text = Regex.Replace(text, "[$]+", "dollar");
            text = Regex.Replace(text, @"@[^\s]+", "username");
            text = Regex.Replace(text, @"[\s]+", " ");

            return text.Split(" @$/#.-:&*+=[]?!(){},''\">_<;%\\".ToCharArray());
        }

        public void Access(IVisitor visitor)
        {
            visitor.TeachNeuralNetwork(this);
        }

    }
}
