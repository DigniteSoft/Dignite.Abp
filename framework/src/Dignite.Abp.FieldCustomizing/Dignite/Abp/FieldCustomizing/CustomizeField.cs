
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeField:ICustomizeField
    {
        protected ICustomizeFieldDefinition FieldDefinition { get;  }

        protected ICustomizeFieldProvider Provider { get; }


        public CustomizeField(
            ICustomizeFieldDefinition fieldDefinition,
            ICustomizeFieldProvider provider)
        {
            FieldDefinition = fieldDefinition;
            Provider = provider;
        }

        public virtual void Validate(
            object value,
             List<ValidationResult> validationErrors)
        {
            Provider.Validate(
                new CustomizeFieldProviderValidateArgs(
                    FieldDefinition,
                    value,
                    validationErrors
                )
            );
        }
    }
}
