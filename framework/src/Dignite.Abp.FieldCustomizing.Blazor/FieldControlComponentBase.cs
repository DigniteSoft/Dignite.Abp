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
            HideFieldLable = false;
        }

        public abstract Type FieldProviderType { get; }

        [Parameter]
        public CustomizeField CustomizeField { get; set; }

        [Parameter]
        public bool HideFieldLable { get; set; }
    }
}
