using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.BlobStoringManagement
{
    public class BlobsAppService_Tests : BlobStoringManagementApplicationTestBase
    {
        private readonly IBlobsAppService _blobsAppService;

        public BlobsAppService_Tests()
        {
            _blobsAppService = GetRequiredService<IBlobsAppService>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            var result = await _blobsAppService.GetListAsync("DignitePost", "0f8fad5b-d9cb-469f-a165-70867728950e");
            //result.Value.ShouldBe(42);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            //var result = await _blobsAppService.GetAuthorizedAsync();
            //result.Value.ShouldBe(42);
        }
    }
}
