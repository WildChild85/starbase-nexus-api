namespace starbase_nexus_api.Models.Api
{
    public interface IPagedList
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; set; }
        int TotalCount { get; set; }
    }
}
