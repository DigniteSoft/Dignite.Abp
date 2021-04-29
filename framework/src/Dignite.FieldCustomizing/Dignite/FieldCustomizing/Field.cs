using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Threading;

namespace Dignite.FieldCustomizing
{
    public class Field:IField
    {
        protected string Name { get; }

        protected FieldConfiguration Configuration { get; }

        protected IFieldProvider Provider { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        protected IServiceProvider ServiceProvider { get; }


        public Field(
            string fieldName,
            FieldConfiguration configuration,
            IFieldProvider provider,
            ICancellationTokenProvider cancellationTokenProvider,
            IServiceProvider serviceProvider)
        {
            Name = fieldName;
            Configuration = configuration;
            Provider = provider;
            CancellationTokenProvider = cancellationTokenProvider;
            ServiceProvider = serviceProvider;
        }

        public virtual async Task<FieldValueValidateResult> ValidateAsync(
            object value,
            CancellationToken cancellationToken = default)
        {
                return await Provider.ValidateAsync(
                    new FieldProviderValidateValueArgs(
                        Name,
                        Configuration,
                        value,
                        CancellationTokenProvider.FallbackToProvider(cancellationToken)
                    )
                );
        }
    }
}
