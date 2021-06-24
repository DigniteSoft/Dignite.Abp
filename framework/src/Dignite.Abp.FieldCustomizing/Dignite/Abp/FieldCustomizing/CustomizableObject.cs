using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public abstract class CustomizableObject : IHasExtraFields, IValidatableObject
    {
        public ExtraFieldDictionary ExtraFields { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();
            var fieldDefinitions = GetFieldDefinitions(validationContext);
            var fieldFactory = validationContext.GetRequiredService<ICustomizeFieldFactory>();

            foreach (var customField in ExtraFields)
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
