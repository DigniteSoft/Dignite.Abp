namespace Dignite.Abp.BlobStoringManagement
{
    public static class BlobStoringManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "BlobStoringManagement";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "BlobStoringManagement";
    }
}
