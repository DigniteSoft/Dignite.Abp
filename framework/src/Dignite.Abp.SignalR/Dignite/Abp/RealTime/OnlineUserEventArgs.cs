namespace Dignite.Abp.RealTime
{
    public class OnlineUserEventArgs : OnlineClientEventArgs
    {
        public IUserIdentifier User { get; }

        public OnlineUserEventArgs(IUserIdentifier user, IOnlineClient client)
            : base(client)
        {
            User = user;
        }
    }
}
