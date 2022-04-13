using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FileManagement
{
    public class SaveRemoteFileInput
    {
        [Required]
        [NotNull] 
        public string Url { get; set; }

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
