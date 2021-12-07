using Dignite.Abp.FieldCustomizing.FieldControls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public abstract class CustomizableObject : IHasCustomizableFields, IValidatableObject
    {
        public CustomizeFieldDictionary CustomizedFields { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();
            var fieldDefinitions = GetFieldDefinitions(validationContext);
            var fieldControlProvider = validationContext.GetRequiredService<IFieldControlProvider>();

            foreach (var customField in CustomizedFields)
            {
                fieldControlProvider.Validate(
                    new FieldControlValidateArgs(
                        fieldDefinitions.Single(fi => fi.Name == customField.Key),
                        customField.Value,
                        validationErrors
                    )
                );
            }

            return validationErrors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IReadOnlyList<BasicCustomizeFieldDefinition> GetFieldDefinitions(ValidationContext validationContext);
    }
}
