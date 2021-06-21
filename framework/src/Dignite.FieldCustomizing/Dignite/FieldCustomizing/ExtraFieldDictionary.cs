using System;
using System.Collections.Generic;

namespace Dignite.FieldCustomizing
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
