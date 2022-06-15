using Dignite.Abp.FieldCustomizing.Localization;
using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldConfigurationComponentBase : AbpComponentBase, IFieldConfigurationComponent, ITransientDependency
    {
        protected FieldConfigurationComponentBase()
        {
            LocalizationResource = typeof(DigniteAbpFieldCustomizingResource);
        }

        public abstract Type FieldProviderType { get; }

        [Parameter]
        public ICustomizeFieldDefinition Definition { get; set; }
    }
}
