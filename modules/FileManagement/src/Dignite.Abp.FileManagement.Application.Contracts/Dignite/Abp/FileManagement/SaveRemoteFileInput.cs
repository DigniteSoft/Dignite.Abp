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
        [StringLength(FileConsts.MaxEntityTypeLength)]
        [NotNull]
        public string EntityType { get; set; }

        [Required]
        [StringLength(FileConsts.MaxEntityIdLength)]
        [NotNull]
        public string EntityId { get; set; }
    }
}
