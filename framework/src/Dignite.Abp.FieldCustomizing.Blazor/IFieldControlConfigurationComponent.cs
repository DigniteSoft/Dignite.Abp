using System;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldControlConfigurationComponent
    {
        Type FieldControlProviderType { get; }

        ICustomizeFieldDefinition Definition { get;  }
    }
}
