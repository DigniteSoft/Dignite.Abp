using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls.Table
{
    public class TableConfiguration:FieldControlConfigurationBase
    {
        [Required]
        public List<TableColumn> TableColumns
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(TableConfigurationNames.TableColumns, new List<TableColumn>());
            set => _fieldControlConfiguration.SetConfiguration(TableConfigurationNames.TableColumns, value);
        }


        public TableConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
