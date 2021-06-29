using JetBrains.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeFieldFormValidateArgs
    {
        public ICustomizeFieldDefinition FieldDefinition { get; }

        public object Value { get; }

        public List<ValidationResult> ValidationErrors { get; }

        public CustomizeFieldFormValidateArgs(
            [NotNull] ICustomizeFieldDefinition fieldDefinition,
            object value,
            List<ValidationResult> validationErrors)
        {
            FieldDefinition = fieldDefinition;
            Value = value;
            ValidationErrors = validationErrors;
        }
    }
}
