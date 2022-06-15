using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Table
{
    public class TableConfiguration:FieldConfigurationBase
    {
        [Required]
        public List<TableColumn> TableColumns
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(TableConfigurationNames.TableColumns, new List<TableColumn>());
            set => _fieldConfiguration.SetConfiguration(TableConfigurationNames.TableColumns, value);
        }


        public TableConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
