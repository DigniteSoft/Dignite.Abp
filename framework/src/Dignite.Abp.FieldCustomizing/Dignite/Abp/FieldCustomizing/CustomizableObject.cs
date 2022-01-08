﻿using Dignite.Abp.FieldCustomizing.FieldControls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public abstract class CustomizableObject : IHasCustomizableFields, IValidatableObject
    {
        [JsonInclude]
        public CustomizeFieldDictionary CustomizedFields { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();
            var fieldDefinitions = GetFieldDefinitions(validationContext);
            var fieldControlProviderSelector = validationContext.GetRequiredService<IFieldControlProviderSelector>();

                foreach (var customField in CustomizedFields)
                {
                    var fieldDefinition = fieldDefinitions.Single(fi => fi.Name == customField.Key);
                    var fieldControlProvider = fieldControlProviderSelector.Get(fieldDefinition.FieldControlProviderName);
                    fieldControlProvider.Validate(
                        new FieldControlValidateArgs(
                            fieldDefinition,
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
