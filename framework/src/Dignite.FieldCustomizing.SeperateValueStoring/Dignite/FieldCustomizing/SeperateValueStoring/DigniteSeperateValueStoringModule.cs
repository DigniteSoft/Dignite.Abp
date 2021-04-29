
using Dignite.FieldCustomizing.SeperateValueStoring.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DigniteFieldCustomizingModule)
    )]
    public class DigniteSeperateValueStoringModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteSeperateValueStoringModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SeperateValueStoringResource>("en")
                    .AddVirtualJson("/Dignite/FieldCustomizing/SeperateValueStoring/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("SeperateValueStoring", typeof(SeperateValueStoringResource));
            });
        }
    }
}