using System;
using System.Runtime.Serialization;

namespace RssReader.Core.Exceptions
{
    [Serializable]
    public class AddRssSourceUrlRequiredException : Exception
    {
        public AddRssSourceUrlRequiredException()
        {
        }

        public AddRssSourceUrlRequiredException(string message) : base(message)
        {
        }

        public AddRssSourceUrlRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddRssSourceUrlRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
