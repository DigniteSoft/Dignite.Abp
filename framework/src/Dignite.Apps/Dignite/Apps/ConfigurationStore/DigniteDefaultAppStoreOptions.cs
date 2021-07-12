namespace Dignite.Apps.ConfigurationStore
{
    public class DigniteDefaultAppStoreOptions
    {
        public AppConfiguration[] Apps { get; set; }

        public DigniteDefaultAppStoreOptions()
        {
            Apps = new AppConfiguration[0];
        }
    }
}