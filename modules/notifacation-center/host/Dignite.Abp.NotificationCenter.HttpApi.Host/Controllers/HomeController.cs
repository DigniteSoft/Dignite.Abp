using Dignite.Abp.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.NotificationCenter.Controllers
{
    public class HomeController : AbpController
    {
        private readonly NotificationPublisher _notificationPublisher;

        public HomeController(NotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }

        public async Task<ActionResult> SendNotificationTest()
        {
            var notificationData = new NotificationData
            {
                ["TestValue"] = 42
            };

            await _notificationPublisher.PublishAsync("TestNotification", 
                notificationData, 
                severity: NotificationSeverity.Success,
                userIds:new Guid[] { Guid.Parse("72739d63-c978-1d8c-6ce1-3a00f88efa62") });
            return Json(new { message = "发送成功" });
        }
    }
}
