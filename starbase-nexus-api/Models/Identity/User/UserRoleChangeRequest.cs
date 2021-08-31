using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Identity.User
{
    public class UserRoleChangeRequest
    {
        [Required]
        public Guid? RoleId { get; set; }
    }
}
