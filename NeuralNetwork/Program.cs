using EntityDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentRepository dr = new DocumentRepository();
            string[] classes = dr.GetDocumentsClasses().ToArray();
            string[] vocabulary = dr.GetVocabulary()
                .Select(x => x.Value)
                .ToArray();
            TextClassifyingNeuralNetwork n = new TextClassifyingNeuralNetwork(classes, vocabulary);
            IVisitor teacher = new NeuralNetworkTeacher();
            n.Access(teacher);

            string path = @"C:\Users\Alexander\Documents\Visual Studio 2013\Projects\EntityDb\EntityDb\bin\Debug\115Cat\test\acq";
            string[] files = Directory.GetFiles(path);
            foreach(var file in files)
            {
                string text = File.ReadAllText(file);

                var watch = Stopwatch.StartNew();

                string answer = n.Parse(text);

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("{0} - {1} ({2}miliseconds)", "acq", answer, elapsedMs);
            }

            Console.ReadKey();
        }
    }
}
