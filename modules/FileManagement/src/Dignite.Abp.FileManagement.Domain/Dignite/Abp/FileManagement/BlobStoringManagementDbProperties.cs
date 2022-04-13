namespace Dignite.Abp.FileManagement
{
    public static class FileManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "DigniteBlobStoring";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "DigniteBlobStoring";
    }
}
