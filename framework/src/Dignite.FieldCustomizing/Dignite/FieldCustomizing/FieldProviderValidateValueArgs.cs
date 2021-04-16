using JetBrains.Annotations;
using System.Threading;

namespace Dignite.FieldCustomizing
{
    public class FieldProviderValidateValueArgs :FieldProviderArgs
    {
        public object Value { get; }

        public FieldProviderValidateValueArgs(
            [NotNull] string fieldName,
            [NotNull] FieldConfiguration configuration,
            object value,
            CancellationToken cancellationToken = default)
            : base(
                fieldName,
                configuration,
                cancellationToken
                )
        {
            Value = value;
        }
    }
}
