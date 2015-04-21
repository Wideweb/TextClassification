using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork;
using EntityDb;
using System.IO;

namespace TextClassificator
{
    class Program
    {
        static void Main(string[] args)
        {string path = @"C:\Users\Alexander\Documents\Visual Studio 2013\Projects\EntityDb\EntityDb\bin\Debug\115Cat\training";
            
            DocumentRepository dr = new DocumentRepository();
            List<Word> vocabulary = dr.GetVocabulary().OrderBy(x => x.Id).ToList();

            string[] classes = GetClasses(@"C:\Users\Alexander\Documents\Visual Studio 2013\Projects\EntityDb\EntityDb\bin\Debug\115Cat\training");
            TextClassifyingNeuralNetwork nw =
                new TextClassifyingNeuralNetwork(classes, vocabulary.Select(x => x.Value).ToArray());


            int docsCount = dr.DocumentsCount();
            int takenDocuments = 0;
            int bufferSize = 2000;

            while(takenDocuments < docsCount)
            {
                List<Document> docs = dr.TakeDocuments(bufferSize, takenDocuments);
                takenDocuments += docs.Count;

                foreach(var doc in docs)
                {
                    nw.Study(doc.Tokens, doc.Classification);
                }
            }
        }

        static string[] GetClasses(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}
