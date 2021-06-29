
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeField:ICustomizeField
    {
        protected ICustomizeFieldDefinition FieldDefinition { get;  }

        protected ICustomizeFieldFormProvider FormProvider { get; }


        public CustomizeField(
            ICustomizeFieldDefinition fieldDefinition,
            ICustomizeFieldFormProvider formProvider)
        {
            FieldDefinition = fieldDefinition;
            FormProvider = formProvider;
        }

        public virtual void Validate(
            object value,
             List<ValidationResult> validationErrors)
        {
            FormProvider.Validate(
                new CustomizeFieldFormValidateArgs(
                    FieldDefinition,
                    value,
                    validationErrors
                )
            );
        }
    }
}
