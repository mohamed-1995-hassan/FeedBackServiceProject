using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Core.Exceptions
{
    [Serializable]
   public class NotFoundException : DomainException
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) : base(message)
        {

        }
        public NotFoundException(string message, Exception inner) : base(message, inner)
        {

        }

        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
