using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.InGame.ItemCategory
{
    public class PatchItemCategory
    {
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? Description { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
    }
}
