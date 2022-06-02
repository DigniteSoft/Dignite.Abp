using Dignite.Abp.FieldCustomizing.Localization;
using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldControlConfigurationComponentBase : AbpComponentBase, IFieldControlConfigurationComponent, ITransientDependency
    {
        protected FieldControlConfigurationComponentBase()
        {
            LocalizationResource = typeof(DigniteAbpFieldCustomizingResource);
        }

        public abstract Type FieldControlProviderType { get; }

        [Parameter]
        public ICustomizeFieldDefinition Definition { get; set; }
    }
}
