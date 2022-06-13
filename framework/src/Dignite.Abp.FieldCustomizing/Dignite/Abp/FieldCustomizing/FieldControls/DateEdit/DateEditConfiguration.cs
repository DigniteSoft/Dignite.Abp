using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls.DateEdit
{
    public class DateEditConfiguration:FieldControlConfigurationBase
    {
        [Required]
        public DateInputMode InputMode
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(DateEditConfigurationNames.InputMode, DateInputMode.Date);
            set => _fieldControlConfiguration.SetConfiguration(DateEditConfigurationNames.InputMode, value);
        }


        public DateTimeOffset? Max
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<DateTimeOffset?>(DateEditConfigurationNames.Max);
            set => _fieldControlConfiguration.SetConfiguration(DateEditConfigurationNames.Max, value);
        }

        public DateTimeOffset? Min
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<DateTimeOffset?>(DateEditConfigurationNames.Min);
            set => _fieldControlConfiguration.SetConfiguration(DateEditConfigurationNames.Min, value);
        }


        public DateEditConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
