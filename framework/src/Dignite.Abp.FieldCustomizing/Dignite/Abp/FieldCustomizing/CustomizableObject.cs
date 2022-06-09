using Dignite.Abp.FieldCustomizing.FieldControls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Volo.Abp.Content;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public abstract class CustomizableObject : IHasCustomizableFields, IValidatableObject
    {
        protected CustomizableObject()
        {
            CustomizedFields=new();
            CustomizedFieldFiles= new();
        }

        [JsonInclude]
        public CustomizeFieldDictionary CustomizedFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, List<IRemoteStreamContent>> CustomizedFieldFiles { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();
            var fieldDefinitions = GetFieldDefinitions(validationContext);
            var fieldControlProviderSelector = validationContext.GetRequiredService<IFieldControlProviderSelector>();
            try
            {
                foreach (var customField in CustomizedFields)
                {
                    var fieldDefinition = fieldDefinitions.FirstOrDefault(fi => fi.Name == customField.Key);
                    if (fieldDefinition == null)
                        continue;
                    var fieldControlProvider = fieldControlProviderSelector.Get(fieldDefinition.FieldControlProviderName);
                    fieldControlProvider.Validate(
                        new FieldControlValidateArgs(
                            fieldDefinition,
                            customField.Value,
                            validationErrors
                        )
                    );
                }
            }
            catch (Exception ex)
            { 
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
