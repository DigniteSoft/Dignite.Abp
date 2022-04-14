using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.Abp.FileManagement
{
    [Serializable]
    public class EntityBlobNotAddableException : BusinessException
    {
        public EntityBlobNotAddableException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public EntityBlobNotAddableException(string entityType)
        {
            Code = FileManagementErrorCodes.Blobs.EntityNotAddable;
            EntityType = entityType;
            WithData(nameof(EntityType), EntityType);
        }

        public string EntityType { get; }
    }
}
