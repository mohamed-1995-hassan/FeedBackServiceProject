﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Core.Exceptions
{
    [Serializable]
    public class DomainException :Exception
    {
        public DomainException()
        {

        }
        public DomainException(string message) : base(message)
        {

        }
        public DomainException(string message, Exception inner) : base(message, inner)
        {

        }

        public DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
