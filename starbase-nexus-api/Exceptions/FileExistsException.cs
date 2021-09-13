using System;

namespace starbase_nexus_api.Exceptions
{
    public class FileExistsException : Exception
    {
        public FileExistsException(string? message) : base(message)
        {
        }
    }
}
