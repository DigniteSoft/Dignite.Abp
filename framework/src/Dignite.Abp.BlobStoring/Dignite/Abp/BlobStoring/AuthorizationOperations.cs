using System;

namespace Dignite.Abp.BlobStoring
{
    [Flags]
    public enum AuthorizationOperations
    {
        /// <summary>
        ///
        /// </summary>
        Saving = 1,

        /// <summary>
        ///
        /// </summary>
        Getting = 2,

        /// <summary>
        ///
        /// </summary>
        Deleting = 4
    }
}
