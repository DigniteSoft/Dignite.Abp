using Microsoft.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldCustomizingBlazorComponentBase:ComponentBase, ITransientDependency
    {
        public abstract string ForFieldControlProviderName { get; }
    }
}
