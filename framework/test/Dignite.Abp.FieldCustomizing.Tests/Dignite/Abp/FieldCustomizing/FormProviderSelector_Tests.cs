using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.Abp.FieldCustomizing.FieldControls.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing
{
    public class FormProviderSelector_Tests: FieldCustomizingTestBase
    {
        private readonly IFieldControlProviderSelector _selector;

        public FormProviderSelector_Tests()
        {
            _selector = GetRequiredService<IFieldControlProviderSelector>();
        }

        [Fact]
        public void Should_Select_Textbox_Form_Provider()
        {
            _selector.Get(TextboxFieldControlProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldControlProvider>();
        }
    }
}