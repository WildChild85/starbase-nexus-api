using System.Collections.Generic;

namespace starbase_nexus_api.Constants.Identity
{
    public static class RoleConstants
    {
        public const string ADMINISTRATOR = "Administrator";
        public const string MODERATOR = "Moderator";

        public const string ADMIN_OR_MODERATOR = "Administrator,Moderator";

        public static readonly List<string> ROLES = new List<string>
        {
            ADMINISTRATOR,
            MODERATOR
        };
    }
}
