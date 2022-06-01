using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Volo.Abp.Content;

namespace Dignite.Abp.BlobStoringManagement
{
    public class SaveStreamInput
    {
        public IRemoteStreamContent FileStream { get; set; }

        public string EntityType { get; set; }

        public string EntityId { get; set; }


        public string FileName { get; set; }
    }
}
