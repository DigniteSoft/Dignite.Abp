namespace Dignite.Abp.FieldCustomizing.Fields.NumericEdit
{
    public class NumericEditConfiguration:FieldConfigurationBase
    {
        /// <summary>
        /// Maximum number of decimal places after the decimal separator.
        /// </summary>
        public int Decimals
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<int>(NumericEditConfigurationNames.Decimals,2);
            set => _fieldConfiguration.SetConfiguration(NumericEditConfigurationNames.Decimals, value);
        }


        public decimal? Max
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<decimal?>(NumericEditConfigurationNames.Max);
            set => _fieldConfiguration.SetConfiguration(NumericEditConfigurationNames.Max, value);
        }

        public decimal? Min
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<decimal?>(NumericEditConfigurationNames.Min);
            set => _fieldConfiguration.SetConfiguration(NumericEditConfigurationNames.Min, value);
        }

        /// <summary>
        /// Specifies the interval between valid values.
        /// </summary>
        public decimal? Step
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<decimal?>(NumericEditConfigurationNames.Step);
            set => _fieldConfiguration.SetConfiguration(NumericEditConfigurationNames.Step, value);
        }

        /// <summary>
        /// Format Specifier
        /// </summary>
        public string FormatSpecifier
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(NumericEditConfigurationNames.FormatSpecifier,null);
            set => _fieldConfiguration.SetConfiguration(NumericEditConfigurationNames.FormatSpecifier, value);
        }

        public NumericEditConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
