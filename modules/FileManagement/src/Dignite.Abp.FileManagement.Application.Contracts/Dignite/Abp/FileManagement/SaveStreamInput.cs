using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace Dignite.Abp.FileManagement
{
    public class SaveStreamInput
    {
        [Required]
        public IRemoteStreamContent File { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxEntityTypeLength)]
        public string EntityType { get; set; }

        [Required]
        [StringLength(BlobConsts.MaxEntityIdLength)]
        public string EntityId { get; set; }
    }
}
