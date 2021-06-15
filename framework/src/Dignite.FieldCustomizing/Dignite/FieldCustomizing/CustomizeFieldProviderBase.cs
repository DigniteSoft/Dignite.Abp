using Dignite.FieldCustomizing.Localization;
using Microsoft.Extensions.Localization;
using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.FieldCustomizing
{
    public abstract class CustomizeFieldProviderBase : ICustomizeFieldProvider
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

        protected IStringLocalizer L
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = CreateLocalizer();
                }

                return _localizer;
            }
        }
        private IStringLocalizer _localizer;

        protected Type LocalizationResource
        {
            get => _localizationResource;
            set
            {
                _localizationResource = value;
                _localizer = null;
            }
        }
        private Type _localizationResource = typeof(FieldCustomizingResource);

        public abstract string Name { get; }

        public abstract string DisplayName { get; }

        public abstract CustomizeFieldType FieldType { get; }

        public abstract void Validate(CustomizeFieldProviderValidateArgs args);


        protected virtual IStringLocalizer CreateLocalizer()
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }
    }
}
