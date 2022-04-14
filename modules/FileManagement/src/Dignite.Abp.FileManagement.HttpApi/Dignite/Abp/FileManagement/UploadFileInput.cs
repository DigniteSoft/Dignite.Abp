using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FileManagement
{
    public class UploadFileInput
    {
        [Required]
        [NotNull]
        public IFormFile File  { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxEntityTypeLength)]
        [NotNull]
        public string EntityType { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxEntityIdLength)]
        [NotNull]
        public string EntityId { get; set; }
    }
}
