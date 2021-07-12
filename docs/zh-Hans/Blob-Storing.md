## Dignite BLOB 存储介绍

Dignite BlobStoring是基于[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)开发，为BLOB存储提供授权验证、 数据流处理管道、BLOB 信息记录等辅助功能。

这些功能是扩展了ABP BlobStoring功能特征，本文着重介绍在ABP BlobStoring功能基础上添加的功能.

## 安装

[Dignite.Abp.BlobStoring](https://www.nuget.org/packages/Dignite.Abp.BlobStoring)是定义BLOB存储服务的主要包. 

在项目中安装 `Dignite.Abp.BlobStoring`NuGet包，然后将`[DependsOn(typeof(DigniteAbpBlobStoringModule))]`添加到项目内的[ABP模块](https://docs.abp.io/zh-Hans/abp/latest/Module-Development-Basics)类中.

## BLOB 名称的生成
INameGenerator
TODO.......

## BLOB存储授权验证

授权验证用于判断是否允许用户执行Blob存储操作。

下面，让我们看看如何扩展'AbpBlobStoringOptions'实现BLOB存储授权验证，容器配置代码如下：

````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.SetAuthorizationHandler<AuthorizationHandler>(authorization =>
        {
            authorization.SavingRoles = new string[] { "admin" };
            authorization.SavingPolicy = "FileManagement.Files.Create";
        });
    });
});
````

上面的示例代码扩展自[配置容器](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing#%E9%85%8D%E7%BD%AE%E5%AE%B9%E5%99%A8)。

当保存 BLOB 时，系统需要当前用户拥有`admin`角色或`FileManagement.Files.Create`权限策略，方能通过BLOB存储授权验证。

除了保存 BLOB 时的授权验证，还可以配置获取、删除 BLOB 时的授权验证：

````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.SetAuthorizationHandler<AuthorizationHandler>(authorization =>
        {
            authorization.SavingRoles = new string[] { "admin" };
            authorization.SavingPolicy = "FileManagement.Files.Create";
            authorization.GettingRoles = new string[] { "user" };
            authorization.GettingPolicy = "FileManagement.Files.Read";
            authorization.DeletingRoles = new string[] { "admin" };
            authorization.DeletingPolicy = "FileManagement.Files.Delete";
        });
    });
});
````

### 基于资源的授权验证

如果需要根据资源判断授权验证，需要开发者写一个继承自`AuthorizationHandler`类的子类，并且重写`CheckSavingPermissionAsync`、`CheckGettingPermissionAsync`、`CheckDeletingPermissionAsync`方法，在其内部当判断没有授权时，抛出`AbpAuthorizationException`。

示例：

````csharp
public class MyCustomAuthorizationHandler : AuthorizationHandler
{
    public override Task CheckSavingPermissionAsync(AuthorizationHandlerConfiguration configuration)
    {
        await base.CheckSavingPermissionAsync();

        //TODO:后面写上你的逻辑代码
        throw new Volo.Abp.Authorization.AbpAuthorizationException("no permission");
    }

    public override Task CheckGettingPermissionAsync(AuthorizationHandlerConfiguration configuration,IBlobInfo blobInfo)
    {
        await base.CheckGettingPermissionAsync();

        //TODO:后面写上你的逻辑代码
        throw new Volo.Abp.Authorization.AbpAuthorizationException("no permission");
    }

    public override Task CheckDeletingPermissionAsync(AuthorizationHandlerConfiguration configuration,IBlobInfo blobInfo)
    {
        await base.CheckDeletingPermissionAsync();

        //TODO:后面写上你的逻辑代码
        throw new Volo.Abp.Authorization.AbpAuthorizationException("no permission");
    }
}
````

修改容器配置代码：
````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.SetAuthorizationHandler<MyCustomAuthorizationHandler>(authorization =>
        {
            authorization.SavingRoles = new string[] { "admin" };
            authorization.SavingPolicy = "FileManagement.Files.Create";
        });
    });
});
````

## Blob 流处理管道

Blob 流处理管道主要用于Blob流存储前的数据处理、逻辑处理过程，常用到的有上传文件类型的分析、文件大小的限制、图片的等比例缩小等。

### Blob 文件类型检查管道

通过容器配置设定允许用户上传的文件类型：
````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.AddFileTypeCheckHandler(fileTypeCheck =>
        {
            fileTypeCheck.AllowedFileTypeNames = new string[] { ".jpeg",".jpg",".png" };
        });
    });
});
````
> 上面Blob 文件类型检查管道的验证逻辑：以BlobName的扩展名与预设的文件类型数组对比，预设文件类型数组包含BlobName扩展名则验证通过，反之抛出异常。
> 基于该验证逻辑，需要确保BlobName含有扩展名，上面的管道方可正常运行。

### 限制Blob流大小管道

通过容器配置限定用户上传文件大小：
````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.AddFileSizeLimitHandler(fileSizeLimit =>
        {
            fileSizeLimit.MaximumFileSize = 10240; //最大允许上传10M大小的文件
        });
    });
});
````

### ResizeImageHandler

通过容器配置限定用户上传文件大小：
````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.AddImageResizeHandler(imageResize =>
        {
            imageResize.ImageWidth  = 1140; //预设图片的宽度值；如果上传的图片宽度大于预设值，采用等比例缩小；
            imageResize.ImageHeight  = 550; //预设图片的高度值；如果上传的图片高度大于预设值，采用等比例缩小；
            imageResize.ImageSizeMustBeLargerThanPreset = false; //上传的图片宽度和高度不可小于预设的尺寸
        });
    });
});
````

> 可不预设图片的宽度和高度值，上传的图片尺寸将不受约束限制。

### 创建自定义Blob流处理管道

第一步：创建一个实现`IBlobProcessHandler`接口继承的类：

````csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Demo
{
    public class MyCustomBlobProcessHandler : IBlobProcessHandler
    {
        public Task ProcessAsync(BlobProcessHandlerContext context)
        {
            //TODO...
            return Task.CompletedTask;
        }
    }
}
````

第二步：创建`BlobContainerConfiguration`的扩展方法：

````csharp
using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Demo
{
    public static class MyCustomHandlerConfigurationExtensions
    {
        public static void AddMyCustomHandler(
            this BlobContainerConfiguration containerConfiguration)
        {
            var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.BlobProcessHandlers,
                new TypeList<IBlobProcessHandler>());

            if (blobProcessHandlers.TryAdd<MyCustomBlobProcessHandler>())
            {
                //TODO...
            }
        }
    }
}
````

#### Blob 流处理管道的配置

如果需要一个可灵活配置Blob 流处理管道的需求，可创建一个`MyCustomBlobProcessHandlerConfiguration`类

````csharp
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class MyCustomBlobProcessHandlerConfiguration
    {
        public string Option1
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>("MyCustomBlobProcessHandlerOptionName", null);
            set => _containerConfiguration.SetConfiguration("MyCustomBlobProcessHandlerOptionName", value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public MyCustomBlobProcessHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
````

然后，在`MyCustomHandlerConfigurationExtensions`类中增加下扩展方法：

````csharp
public static MyCustomBlobProcessHandlerConfiguration GetMyCustomBlobProcessHandlerConfiguration(
    this BlobContainerConfiguration containerConfiguration)
{
    return new MyCustomBlobProcessHandlerConfiguration(containerConfiguration);
}
````

## Blob 信息

当需要记录Blob信息时，可以创建一个实现`IBlobInfoStore`接口的继承类，示例如下：
````csharp
using System.Threading.Tasks;

namespace Dignite.Demo
{
    public class MyBlobInfoStore:IBlobInfoStore
    {
        //TODO:基于数据库的BLOB信息记录，添加数据仓储的依赖注入
        
        
        /// <summary>
        /// 判断指定blobName的 BLOB 是否存在
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public Task<bool> AnyAsync(string containerName, string blobName)
        {
            //TODO...
        }

        /// <summary>
        /// 判断指定hash的 BLOB 是否存在
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public Task<bool> AnyByHashAsync(string containerName, string hash)
        {
            //TODO...
        }

        /// <summary>
        /// 获取指定hash的主本 BLOB 
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public async Task<IBlobInfo> GetMainAsync(string containerName, string hash)
        {
            //TODO...
        }

        /// <summary>
        /// 创建<see cref="IBlobInfo"/>记录
        /// </summary>
        /// <param name="blobInfo"></param>
        /// <returns></returns>
        public Task CreateAsync(IBlobInfo blobInfo)
        {
            //TODO...
        }
    }
}

````

然后，在容器配置中设定BLOB信息存储：
````csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<ProfilePictureContainer>(container =>
    {
        container.SetBlobInfoStore<MyBlobInfoStore>();
    });
});
````

### NullBlobInfoStore
默认情况下，或容器配置中设定Blob信息存储类为`NullBlobInfoStore`，那么系统不记录BLOB信息。


