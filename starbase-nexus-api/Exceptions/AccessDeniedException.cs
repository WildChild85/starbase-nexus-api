using System;

namespace starbase_nexus_api.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string? message) : base(message)
        {

        }
    }
}
