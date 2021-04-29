using System.Threading;
using System.Threading.Tasks;

namespace Dignite.FieldCustomizing
{
    public interface IField
    {
        Task<FieldValueValidateResult> ValidateAsync(object value, CancellationToken cancellationToken = default);
    }
}
