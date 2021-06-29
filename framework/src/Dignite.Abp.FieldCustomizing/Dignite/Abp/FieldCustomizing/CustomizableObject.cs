using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public abstract class CustomizableObject : IHasCustomizedFields, IValidatableObject
    {
        public CustomizedFieldDictionary CustomizedFields { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();
            var fieldDefinitions = GetFieldDefinitions(validationContext);
            var fieldFactory = validationContext.GetRequiredService<ICustomizeFieldFactory>();

            foreach (var customField in CustomizedFields)
            {
                var field = fieldFactory.Create(fieldDefinitions.Single(fi => fi.Name == customField.Key));
                field.Validate(customField.Value, validationErrors);
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
