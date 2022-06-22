

namespace Dignite.Abp.FieldCustomizing.Fields.Upload
{
    public class File
    {
        public File()
        {
        }

        public File(string entityType, string entityId, string containerName, string blobName, long binarySize, string fileName)
        {
            EntityType = entityType;
            EntityId = entityId;
            ContainerName = containerName;
            BlobName = blobName;
            BinarySize = binarySize;
            FileName = fileName;
        }

        public virtual string EntityType { get;  set; }

        public virtual string EntityId { get;  set; }

        public string ContainerName { get;  set; }

        public string BlobName { get;  set; }

        public long BinarySize { get;  set; }

        public string FileName { get;  set; }
    }
}
