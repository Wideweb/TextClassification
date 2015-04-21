using EntityDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetworkTeacher : IVisitor
    {
        public void TeachNeuralNetwork(TextClassifyingNeuralNetwork neuralNetwork)
        {
            DocumentRepository dr = new DocumentRepository();
            List<Document> docs;
            int takenDocs = 0;
            int bufferSize = 1000;
            int docsCount = dr.DocumentsCount();

            while (takenDocs < docsCount)
            {
                docs = dr.TakeDocuments(bufferSize, takenDocs);
                takenDocs += docs.Count;

                foreach (var doc in docs)
                {
                    neuralNetwork.Study(doc.Tokens, doc.Classification);
                }
            }
        }
    }
}
