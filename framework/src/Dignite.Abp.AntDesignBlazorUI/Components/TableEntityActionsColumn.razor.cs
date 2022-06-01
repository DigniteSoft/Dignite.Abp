using AntDesign;
using System.Threading.Tasks;

namespace Dignite.Abp.AntDesignBlazorUI.Components
{
    public partial class TableEntityActionsColumn<TItem> : ActionColumn
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await SetDefaultValuesAsync();
        }

        protected virtual ValueTask SetDefaultValuesAsync()
        {
            Width = "150px";
            return ValueTask.CompletedTask;
        }
    }
}