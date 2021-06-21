using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.Abp.BlobStoringManagement
{
    [Serializable]
    public class EntityBlobNotAddableException : BusinessException
    {
        public EntityBlobNotAddableException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public EntityBlobNotAddableException(string entityType)
        {
            Code = BlobStoringManagementErrorCodes.Blobs.EntityNotAddable;
            EntityType = entityType;
            WithData(nameof(EntityType), EntityType);
        }

        public string EntityType { get; }
    }
}
