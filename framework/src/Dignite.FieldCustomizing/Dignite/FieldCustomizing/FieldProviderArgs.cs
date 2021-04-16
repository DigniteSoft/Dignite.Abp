using JetBrains.Annotations;
using System.Threading;
using Volo.Abp;

namespace Dignite.FieldCustomizing
{
    public abstract class FieldProviderArgs
    {
        [NotNull]
        public string FieldName { get; }

        [NotNull]
        public FieldConfiguration Configuration { get; }


        public CancellationToken CancellationToken { get; }

        protected FieldProviderArgs(
            [NotNull] string fieldName,
            [NotNull] FieldConfiguration configuration,
            CancellationToken cancellationToken = default)
        {
            FieldName = Check.NotNullOrWhiteSpace(fieldName,nameof(fieldName));
            Configuration = Check.NotNull(configuration, nameof(configuration));
            CancellationToken = cancellationToken;
        }
    }
}
