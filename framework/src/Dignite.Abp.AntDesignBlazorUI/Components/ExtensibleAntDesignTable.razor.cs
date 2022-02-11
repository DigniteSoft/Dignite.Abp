using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using Blazorise.Extensions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using AntDesign.TableModels;

namespace Dignite.Abp.AntDesignBlazorUI.Components
{
    public partial class ExtensibleAntDesignTable<TItem> : ComponentBase
    {
        protected const string DataFieldAttributeName = "DataSource";

        protected Dictionary<string, TableEntityActionsColumn<TItem>> ActionColumns =
            new Dictionary<string, TableEntityActionsColumn<TItem>>();

        protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");
        protected bool Loading = false;

        [Parameter] public IEnumerable<TItem> Data { get; set; }

        [Parameter] public EventCallback<QueryModel<TItem>> ReadData { get; set; }

        [Parameter] public int? TotalItems { get; set; }

        [Parameter] public int PageSize { get; set; }

        [Parameter] public IEnumerable<TableColumn> Columns { get; set; }

        [Parameter] public int CurrentPage { get; set; } = 1;

        [Inject]
        public IStringLocalizerFactory StringLocalizerFactory { get; set; }

        protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
        {
            return (builder) =>
            {
                builder.OpenComponent(type);
                builder.AddAttribute(0, DataFieldAttributeName, data);
                builder.CloseComponent();
            };
        }

        protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
        {
            var convertedValue = columnDefinition.ValueConverter.Invoke(item);
            if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
            {
                return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat,
                    convertedValue);
            }

            return convertedValue;
        }
    }

}
