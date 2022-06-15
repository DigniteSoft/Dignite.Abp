using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.Fields.DataDictionary
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DataDictionary
    {
        public DataDictionary()
        {
            IsActive = true;
            IsStatic = false;
        }

        public DataDictionary(Guid id, string displayName, bool isActive=true, bool isStatic=false)
        {
            Id = id;
            DisplayName = displayName;
            IsActive = isActive;
            IsStatic = isStatic;
        }

        /// <summary>
        /// Id of this Data
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Display name of this Data.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Is the data dictionary item actived.
        /// Active data dictionary are available for use.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Is the data dictionary item static.
        /// Static data dictionaries are not allowed to be deleted or modified.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DataDictionary> Children { get; set; }
    }
}
