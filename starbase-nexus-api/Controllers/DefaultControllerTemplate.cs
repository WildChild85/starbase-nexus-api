using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Models.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace starbase_nexus_api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class DefaultControllerTemplate : ControllerBase
    {
        protected virtual void SetPaginationHeaders(IPagedList pagedList)
        {
            Response.Headers.Add("Pagination.TotalCount", pagedList.TotalCount.ToString());
            Response.Headers.Add("Pagination.PageSize", pagedList.PageSize.ToString());
            Response.Headers.Add("Pagination.Page", pagedList.Page.ToString());
            Response.Headers.Add("Pagination.TotalPages", pagedList.TotalPages.ToString());
        }

        protected virtual bool CurrentUserHasRole(string role)
        {
            return HttpContext.User.IsInRole(role);
        }

        protected virtual string? GetCurrentUserId()
        {
            return (from claim in HttpContext.User.Claims where claim.Type == JwtClaims.ID select claim.Value).FirstOrDefault();
        }
    }
}
