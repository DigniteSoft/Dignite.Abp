using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldComponentBase : AbpComponentBase, IFieldComponent, ITransientDependency
    {
        protected FieldComponentBase()
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
