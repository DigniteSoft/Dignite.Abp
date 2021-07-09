using Dignite.Abp.FieldCustomizing.TextboxForm;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing
{
    public class FormProviderSelector_Tests: FieldCustomizingTestBase
    {
        private readonly IFormProviderSelector _selector;

        public FormProviderSelector_Tests()
        {
            _selector = GetRequiredService<IFormProviderSelector>();
        }

        [Fact]
        public void Should_Select_Textbox_Form_Provider()
        {
            _selector.Get(TextboxFormProvider.ProviderName).ShouldBeAssignableTo<TextboxFormProvider>();
        }
    }
}