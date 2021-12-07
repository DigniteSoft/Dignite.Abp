using Dignite.Abp.FieldCustomizing.Localization;
using Microsoft.Extensions.Localization;
using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public abstract class FieldControlProviderBase : IFieldControlProvider, ITransientDependency
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
        private Type _localizationResource = typeof(DigniteAbpFieldCustomizingResource);

        public abstract string Name { get; }

        public abstract string DisplayName { get; }

        public abstract FieldControlType ControlType { get; }

        public abstract void Validate(FieldControlValidateArgs args);

        public abstract FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration);

        protected virtual IStringLocalizer CreateLocalizer()
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }
    }
}
