using Dignite.Abp.Notifications.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Notifications
{
    public class NotificationSettingProvider : SettingDefinitionProvider
    {

        public override void Define(ISettingDefinitionContext context)
        {
            var definitions = new SettingDefinition[] {
                new SettingDefinition(
                    name:NotificationSettingNames.ReceiveNotifications,
                    defaultValue:"true",
                    displayName:L("ReceiveNotifications"),
                    isVisibleToClients:true)

            };

            context.Add(definitions);
        }

        private static ILocalizableString L(string name)
        {
            return LocalizableString.Create<DigniteNotificationsResource>(name);
        }
    }
}
