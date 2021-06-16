using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.FieldCustomizing
{
    public interface ICustomizeField
    {
        void Validate(object value, List<ValidationResult> validationErrors);
    }
}
