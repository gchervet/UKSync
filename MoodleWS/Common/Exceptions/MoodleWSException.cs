using System;

namespace MoodleWS.Common.Exceptions
{
    public class MoodleWSException : Exception
    {
        public MoodleWSException(string ex) : base(ex)
        {
            
        }
    }
}
