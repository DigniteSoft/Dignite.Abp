using Dignite.Abp.BlobStoring.Fakes;
using Dignite.Abp.BlobStoring.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;


namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(DigniteAbpBlobStoringModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule)
        )]
    public class AbpBlobStoringTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider1>());
            
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers
                    .ConfigureDefault(container =>
                    {
                        container.ProviderType = typeof(FakeBlobProvider1);
                    })
                    .Configure<TestContainer1>(container =>
                     {
                         container.SetAuthorizationHandler<AuthorizationHandler>();
                     })
                    .Configure<TestContainer2>(container =>
                     {
                         container.AddFileSizeLimitHandler(config =>
                            config.MaximumFileSize = 1/1024
                         );
                     })
                    .Configure<TestContainer3>(container =>
                     {
                         container.AddFileTypeCheckHandler(config=>
                            config.AllowedFileTypeNames=new string[] { ".jpeg"}
                         );
                     })
                    .Configure<TestContainer4>(container =>
                    {
                        container.AddImageResizeHandler(imageResize =>
                        {
                            imageResize.ImageWidth = 200; 
                            imageResize.ImageHeight = 200; 
                            imageResize.ImageSizeMustBeLargerThanPreset = false; 
                        });
                    });
            });
        }
    }
}