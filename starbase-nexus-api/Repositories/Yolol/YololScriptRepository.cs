using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Yolol.YololScript;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Yolol
{
    public class YololScriptRepository : UuidBaseRepository<YololScript>, IYololScriptRepository<YololScript>
    {
        public YololScriptRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<YololScript>> GetMultiple(YololScriptSearchParameters parameters)
        {
            IQueryable<YololScript> collection = _dbSet as IQueryable<YololScript>;

            if (parameters.ProjectIds != null)
            {
                List<Guid> projectIds = TextService.GetGuidArray(parameters.ProjectIds, ',').ToList();
                if (projectIds.Count > 0)
                {
                    collection = collection.Where(r => projectIds.Contains(r.ProjectId));
                }
            }

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<YololScript> pagedList = await PagedList<YololScript>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
