using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.BlobStoringManagement
{
    public class SaveBytesInput
    {
        [Required]
        [NotNull] 
        public byte[] Bytes { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxEntityTypeLength)]
        [NotNull]
        public string EntityType { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxEntityIdLength)]
        [NotNull]
        public string EntityId { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxBlobFileNameLength)]
        [NotNull]
        public string FileName { get; set; }
    }
}
