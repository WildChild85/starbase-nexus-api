using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Api
{
    public class SearchParameters
    {
        public string? SearchQuery { get; set; }

        [Range(1, double.PositiveInfinity)]
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 25;

        public string OrderBy { get; set; } = "CreatedAt desc";

        public string Fields { get; set; } = "";
    }
}
