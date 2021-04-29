
using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    public interface IFieldDefinition
    {
        Guid Id { get; }

        string DisplayName { get;  }

        string Name { get;  }


        FieldConfiguration Configuration { get;  }
    }
}
