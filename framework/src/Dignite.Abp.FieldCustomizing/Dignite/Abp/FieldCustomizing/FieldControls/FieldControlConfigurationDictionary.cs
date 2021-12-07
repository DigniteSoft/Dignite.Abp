using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    [Serializable]
    public class FieldControlConfigurationDictionary : Dictionary<string, string>
    {

        public FieldControlConfigurationDictionary()
        {

        }

        public FieldControlConfigurationDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }
    }
}

