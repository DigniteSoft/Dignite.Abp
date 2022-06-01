using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldControlComponentBase:AbpComponentBase, IFieldControlComponent, ITransientDependency
    {
        protected FieldControlComponentBase()
        {
            LocalizationResource = typeof(DigniteAbpFieldCustomizingModule);
        }

        public abstract Type FieldControlProviderType { get; }

        [Parameter]
        public CustomizeField CustomizeField { get; set; }
    }
}
