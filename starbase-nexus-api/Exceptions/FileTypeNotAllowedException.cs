using System;

namespace starbase_nexus_api.Exceptions
{
    public class FileTypeNotAllowedException : Exception
    {
        public FileTypeNotAllowedException(string? message) : base(message)
        {
        }
    }
}
