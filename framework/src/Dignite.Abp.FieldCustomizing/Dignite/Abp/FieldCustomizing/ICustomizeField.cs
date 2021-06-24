using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    public interface ICustomizeField
    {
        void Validate(object value, List<ValidationResult> validationErrors);
    }
}
