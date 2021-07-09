
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeField:ICustomizeField
    {
        protected BasicCustomizeFieldDefinition FieldDefinition { get;  }

        protected IFormProvider FormProvider { get; }


        public CustomizeField(
            BasicCustomizeFieldDefinition fieldDefinition,
            IFormProvider formProvider)
        {
            FieldDefinition = fieldDefinition;
            FormProvider = formProvider;
        }

        public virtual void Validate(
            object value,
             List<ValidationResult> validationErrors)
        {
            FormProvider.Validate(
                new FormValidateArgs(
                    FieldDefinition,
                    value,
                    validationErrors
                )
            );
        }
    }
}
