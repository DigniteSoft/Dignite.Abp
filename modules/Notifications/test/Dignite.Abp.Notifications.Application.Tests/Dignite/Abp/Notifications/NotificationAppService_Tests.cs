using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.Notifications
{
    public class NotificationAppService_Tests : NotificationsApplicationTestBase
    {
        private readonly INotificationAppService _notificationsAppService;

        public NotificationAppService_Tests()
        {
            _notificationsAppService = GetRequiredService<INotificationAppService>();
        }

        /*
        [Fact]
        public async Task GetAsync()
        {
            var result = await _notificationsAppService.GetAsync();
            result.Value.ShouldBe(42);
        }

        [Fact]
        public async Task GetAuthorizedAsync()
        {
            var result = await _notificationsAppService.GetAuthorizedAsync();
            result.Value.ShouldBe(42);
        }*/
    }
}
