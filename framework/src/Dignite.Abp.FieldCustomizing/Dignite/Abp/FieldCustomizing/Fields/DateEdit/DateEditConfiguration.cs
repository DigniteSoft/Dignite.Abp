using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.DateEdit
{
    public class DateEditConfiguration:FieldConfigurationBase
    {
        [Required]
        public DateInputMode InputMode
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(DateEditConfigurationNames.InputMode, DateInputMode.Date);
            set => _fieldConfiguration.SetConfiguration(DateEditConfigurationNames.InputMode, value);
        }


        public DateTimeOffset? Max
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<DateTimeOffset?>(DateEditConfigurationNames.Max);
            set => _fieldConfiguration.SetConfiguration(DateEditConfigurationNames.Max, value);
        }

        public DateTimeOffset? Min
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<DateTimeOffset?>(DateEditConfigurationNames.Min);
            set => _fieldConfiguration.SetConfiguration(DateEditConfigurationNames.Min, value);
        }


        public DateEditConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
