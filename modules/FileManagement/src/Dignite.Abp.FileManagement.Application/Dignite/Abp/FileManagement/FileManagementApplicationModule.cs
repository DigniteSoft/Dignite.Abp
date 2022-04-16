using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BlobStoring;
using Dignite.Abp.BlobStoring;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Volo.Abp.AspNetCore.ExceptionHandling;

namespace Dignite.Abp.FileManagement
{
    [DependsOn(
        typeof(FileManagementDomainModule),
        typeof(FileManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class FileManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<FileManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FileManagementApplicationModule>(validate: true);
            });

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.SetBlobInfoStore<FileInfoStore>();
                });
            });

            Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = true;
                options.SendStackTraceToClients = false;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        await context.Response.WriteAsync(errorFeature.Error.Message);
                    }
                });
            });
            base.OnApplicationInitialization(context);
        }

    }
}
