using System.Threading.Tasks;

namespace Dignite.Abp.Demo.Data
{
    public interface IDemoDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
