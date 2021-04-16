using System.Threading;
using System.Threading.Tasks;

namespace Dignite.FieldCustomizing
{
    public interface IField
    {
        Task<FieldValueValidateResult> ValidateValueAsync(object value, CancellationToken cancellationToken);
    }
}
