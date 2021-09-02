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
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Identity
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DbSet<Role> _roles;

        public RoleRepository(MainDbContext context, RoleManager<Role> roleManager)
        {
            _roles = context.Roles;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Create(Role role)
        {
            role.CreatedAt = DateTimeOffset.UtcNow;
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> Update(Role role)
        {
            role.UpdatedAt = DateTimeOffset.UtcNow;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> Delete(Role role)
        {
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<Role?> GetOneOrDefault(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<Role?> GetOneOrDefaultByMame(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<IEnumerable<Role>> GetMultipleByNames(IEnumerable<string> names, ShapingParameters parameters)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names));

            IQueryable<Role> collection = _roles as IQueryable<Role>;
            List<Role> roles = await collection.Where(r => names.Contains(r.Name)).ApplySort(parameters.OrderBy).ToListAsync();

            return roles;
        }

        public async Task<IEnumerable<Role>> GetMultiple(IEnumerable<string> roleIds, ShapingParameters parameters)
        {
            if (roleIds == null)
                throw new ArgumentNullException(nameof(roleIds));

            IQueryable<Role> collection = _roles as IQueryable<Role>;
            List<Role> roles = await collection.Where(r => roleIds.Contains(r.Id)).ApplySort(parameters.OrderBy).ToListAsync();

            return roles;
        }

        public async Task<PagedList<Role>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<Role> collection = _roles as IQueryable<Role>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r => r.Name.Contains(parameters.SearchQuery));
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Role> pagedList = await PagedList<Role>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async Task<bool> RoleExists(string roleId)
        {
            IQueryable<Role> collection = _roles as IQueryable<Role>;
            return await collection.AnyAsync(r => r.Id == roleId);
        }
    }
}
