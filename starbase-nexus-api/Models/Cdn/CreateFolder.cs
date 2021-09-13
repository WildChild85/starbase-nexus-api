using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Cdn
{
    public class CreateFolder
    {
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string FolderName { get; set; }

        [MaxLength(255)]
        public string Path { get; set; }
    }
}
