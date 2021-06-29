using System;
using System.Collections.Generic;
using Dignite.Abp.FieldCustomizing.TextboxForm;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.MultiTenancy.ConfigurationStore;
using Xunit;

namespace Dignite.Abp.FieldCustomizing
{
    public class FormProviders_Tests : FieldCustomizingTestBase
    {
        private readonly ICustomizeFieldFormProvider _formProviders;


        public FormProviders_Tests()
        {
            _formProviders = ServiceProvider.GetRequiredService<ICustomizeFieldFormProvider>();
        }

        [Fact]
        public void Get_All_FormProvider()
        {
            _formProviders.Name.ShouldBeEquivalentTo("TextboxForm");
        }
    }
}
