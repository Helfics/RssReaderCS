using System;
using System.Runtime.Serialization;

namespace RssReader.Core.Exceptions
{
    [Serializable]
    public class AddRssSourceTitleRequiredException : Exception
    {
        public AddRssSourceTitleRequiredException()
        {
        }

        public AddRssSourceTitleRequiredException(string message) : base(message)
        {
        }

        public AddRssSourceTitleRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddRssSourceTitleRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
