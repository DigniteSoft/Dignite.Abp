
namespace Dignite.Abp.BlobStoring
{
    public interface ICurrentBlobInfoAccessor
    {
        IBlobInfo Current { get; set; }
    }
}
