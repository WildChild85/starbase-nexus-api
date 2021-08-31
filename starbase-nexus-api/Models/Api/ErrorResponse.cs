using System.Collections.Generic;

namespace starbase_nexus_api.Models.Api
{
    public class ErrorResponse
    {
        public ErrorResponse(List<string> errorCodes)
        {
            ErrorCodes = errorCodes;
        }

        public List<string> ErrorCodes { get; set; }
    }
}
