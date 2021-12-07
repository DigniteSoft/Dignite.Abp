using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.FieldControls.DataDictionary
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

        public DataDictionary(string code, string displayName, bool isActive=true, bool isStatic=false)
        {
            Code = code;
            DisplayName = displayName;
            IsActive = isActive;
            IsStatic = isStatic;
        }

        /// <summary>
        /// Hierarchical Code of this data dictionary.
        /// Example: "00001.00042.00005".
        /// This is a unique code for an DataDictionary.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Display name of this DataDictionary.
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
