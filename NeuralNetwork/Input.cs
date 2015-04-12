using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFIDFExample
{
    class Input
    {
        public Link[] OutgoingLinks;

        public Input(int outgoingLinksCount)
        {
            OutgoingLinks = new Link[outgoingLinksCount];
        }
    }
}
