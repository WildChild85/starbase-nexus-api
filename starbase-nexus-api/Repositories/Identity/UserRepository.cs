using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using starbase_nexus_api.Models.Authentication.Discord;

namespace starbase_nexus_api.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        private readonly UserManager<User> _userManager;


        public UserRepository(MainDbContext context, UserManager<User> userManager)
        {
            _users = context.Users;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Create(User user, string plainPassword)
        {
            user.CreatedAt = DateTimeOffset.UtcNow;
            return await _userManager.CreateAsync(user, plainPassword);
        }

        public async Task<IdentityResult> Update(User user)
        {
            user.UpdatedAt = DateTimeOffset.UtcNow;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<User?> GetOneOrDefault(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> GetOneOrDefaultByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User?> GetOneOrDefaultByUserInfo(DiscordMeResponse discordUserInfo)
        {
            IQueryable<User> collection = _users as IQueryable<User>;

            if (discordUserInfo.id == null || discordUserInfo.discriminator == null)
                return null;

            User? user = await collection.Where(u => u.DiscordId == discordUserInfo.id).FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetOneOrDefault(ClaimsPrincipal claimsPrincipal)
        {
            foreach (Claim claim in claimsPrincipal.Claims)
            {
                if (claim.Type.ToLower() == "id")
                {
                    return await _userManager.FindByIdAsync(claim.Value);
                }
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetMultiple(IEnumerable<string> userIds, ShapingParameters parameters)
        {
            if (userIds == null)
                throw new ArgumentNullException(nameof(userIds));

            IQueryable<User> collection = _users as IQueryable<User>;
            List<User> users = await collection.Where(u => userIds.Contains(u.Id)).ApplySort(parameters.OrderBy).ToListAsync();

            return users;
        }

        public async Task<PagedList<User>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<User> collection = _users as IQueryable<User>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r => r.UserName.Contains(parameters.SearchQuery));
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<User> pagedList = await PagedList<User>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async Task<string> GetEmailConfirmationToken(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmail(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GetPasswordResetToken(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetUserPassword(User user, string passwordResetToken, string plainPassword)
        {
            return await _userManager.ResetPasswordAsync(user, passwordResetToken, plainPassword);
        }

        public async Task<IdentityResult> SetUserLockout(User user, bool lockout)
        {
            return await _userManager.SetLockoutEnabledAsync(user, lockout);
        }

        public async Task<IdentityResult> AssignUserToRole(User user, Role role)
        {
            return await _userManager.AddToRoleAsync(user, role.NormalizedName);
        }

        public async Task<IdentityResult> RemoveUserFromRole(User user, Role role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role.NormalizedName);
        }

        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
