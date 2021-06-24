using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public class ExtraFieldDictionary : Dictionary<string, object>
    {
        public ExtraFieldDictionary()
        {

        }

        public ExtraFieldDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }
    }
}
