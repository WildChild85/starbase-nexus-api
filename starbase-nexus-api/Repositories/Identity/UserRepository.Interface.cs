using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Api;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using starbase_nexus_api.Models.Authentication.Discord;

namespace starbase_nexus_api.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<IdentityResult> AssignUserToRole(User user, Role role);
        Task<IdentityResult> ConfirmEmail(User user, string token);
        Task<IdentityResult> Create(User user, string plainPassword);
        Task<string> GetEmailConfirmationToken(User user);
        Task<IEnumerable<User>> GetMultiple(IEnumerable<string> userIds, ShapingParameters parameters);
        Task<PagedList<User>> GetMultiple(SearchParameters parameters);
        Task<User?> GetOneOrDefault(ClaimsPrincipal claimsPrincipal);
        Task<User?> GetOneOrDefault(string id);
        Task<User?> GetOneOrDefaultByName(string username);
        Task<User?> GetOneOrDefaultByUserInfo(DiscordMeResponse discordUserInfo);
        Task<string> GetPasswordResetToken(User user);
        Task<IdentityResult> RemoveUserFromRole(User user, Role role);
        Task<IdentityResult> SetUserLockout(User user, bool lockout);
        Task<IdentityResult> Update(User user);
        Task<IdentityResult> ResetUserPassword(User user, string passwordResetToken, string plainPassword);
        Task<IEnumerable<string>> GetUserRoles(User user);
    }
}