using Volo.Abp.Collections;

namespace Dignite.Abp.Notifications
{
    public class NotificationOptions
    {
        public ITypeList<INotificationDefinitionProvider> DefinitionProviders { get; private set; }

        public ITypeList<IRealTimeNotifier> Notifiers { get; private set; }

        public NotificationOptions()
        {
            DefinitionProviders = new TypeList<INotificationDefinitionProvider>();
            Notifiers = new TypeList<IRealTimeNotifier>();
        }
    }
}