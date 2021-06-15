using JetBrains.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.FieldCustomizing
{
    public class CustomizeFieldProviderValidateArgs
    {
        public ICustomizeFieldDefinition FieldDefinition { get; }

        public object Value { get; }

        public List<ValidationResult> ValidationErrors { get; }

        public CustomizeFieldProviderValidateArgs(
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
