using Volo.Abp.Localization;

namespace Dignite.FieldCustomizing
{
    public class FieldValueValidateError
    {
        public FieldValueValidateError(string code, ILocalizableString description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; private set; }

        public ILocalizableString Description { get; private set; }
    }
}
