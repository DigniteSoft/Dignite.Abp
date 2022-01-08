using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldControlComponentBase:ComponentBase, IFieldControlComponent, ITransientDependency
    {
        public abstract Type FieldControlProviderType { get; }

        [Parameter]
        public FieldControlComponentParameter Parameter { get; set; }
    }
}
