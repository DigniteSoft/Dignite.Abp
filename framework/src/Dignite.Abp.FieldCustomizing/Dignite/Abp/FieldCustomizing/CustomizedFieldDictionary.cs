using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public class CustomizedFieldDictionary : Dictionary<string, object>
    {
        public CustomizedFieldDictionary()
        {

        }

        public CustomizedFieldDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }
    }
}
