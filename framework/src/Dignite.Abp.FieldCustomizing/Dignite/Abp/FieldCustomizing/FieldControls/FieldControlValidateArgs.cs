using JetBrains.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public class FieldControlValidateArgs
    {
        public BasicCustomizeFieldDefinition FieldDefinition { get; }

        public object Value { get; }

        public List<ValidationResult> ValidationErrors { get; }

        public FieldControlValidateArgs(
            [NotNull] BasicCustomizeFieldDefinition fieldDefinition,
            object value,
            List<ValidationResult> validationErrors)
        {
            FieldDefinition = fieldDefinition;
            Value = value;
            ValidationErrors = validationErrors;
        }
    }
}
