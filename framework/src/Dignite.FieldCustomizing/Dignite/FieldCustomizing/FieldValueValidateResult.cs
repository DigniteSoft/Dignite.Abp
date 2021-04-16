using System.Collections.Generic;

namespace Dignite.FieldCustomizing
{
    public class FieldValueValidateResult
    {
        public FieldValueValidateResult()
        {
            Succeeded = true;
            Errors = new List<FieldValueValidateError>();
        }

        public bool Succeeded { get; private set; }

        public List<FieldValueValidateError> Errors { get; private set; }
    }
}
