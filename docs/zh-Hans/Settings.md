# Settings

`Dignite.Abp.Settings`与`Volo.AbpSettings`无缝集成，它添加了设置管理导航以及配置设置项表单的功能, 实现集中统一管理设置。

在阅读文档前，请仔细学习`[Volo.AbpSettings](https://docs.abp.io/zh-Hans/abp/latest/Settings)`的使用方法。

## 安装

[Dignite.Abp.Settings](https://www.nuget.org/packages/Dignite.Abp.Settings)是定义Settings服务的主包.

在项目中安装 `Dignite.Abp.Settings`NuGet包，然后添加`[DependsOn(typeof(DigniteAbpSettingsModule))]`模块依赖：

````csharp
using Volo.Abp.Modularity;
using Dignite.Abp.Settings;

namespace MyCompany.MyProject
{
    [DependsOn(typeof(DigniteAbpSettingsModule))]
    public class MyModule : AbpModule
    {
        //...
    }
}
````

## 定义设置

为了降低学习成本，`Dignite.Abp.Settings`创建了一些`Volo.AbpSettings`同名的接口、类，在保留原使用方法的前提下，只需要更新命名空间即可正常使用。

### 添加设置导航

通常情况下，一个系统由多个Abp Module组成，每个Abp Module中都有自己的设置项目，这些设置统一管理时，为Abp Module Settings添加导航变得很有必要。

定义含有设置导航的设置示例代码如下：

````csharp
using Dignite.Abp.Settings;

namespace MyCompany.MyProject
{
    public class EmailSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingNavigation("Email",L("Email")),
                new Volo.Abp.Settings.SettingDefinition("Smtp.Host", "127.0.0.1"),                
                new Volo.Abp.Settings.SettingDefinition("Smtp.Port", "25"),
                new Volo.Abp.Settings.SettingDefinition("Smtp.UserName"),
                new Volo.Abp.Settings.SettingDefinition("Smtp.Password", isEncrypted: true),
                new Volo.Abp.Settings.SettingDefinition("Smtp.EnableSsl", "false")
            ) ;
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MyProjectResource>(name);
        }
    }
}
````

同样，ABP会自动发现并注册设置的定义。

> 以上示例代码除了使用了同名的`SettingDefinitionProvider`抽象类和为设置添加了`SettingNavigation`之外，其他的使用方法与`Volo.AbpSettings`一致。

### 设置表单

引入`Dignite.Abp.FieldCustomizing`模块，利用它的表单引擎为设置项目配置表单。

#### 安装`Dignite.Abp.FieldCustomizing`模块

在项目中安装 `Dignite.Abp.FieldCustomizing`NuGet包，然后添加`[DependsOn(typeof(DigniteAbpFieldCustomizingModule))]`模块依赖：

````csharp
using Volo.Abp.Modularity;
using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing;

namespace MyCompany.MyProject
{
    [DependsOn(typeof(DigniteAbpSettingsModule))],
    [DependsOn(typeof(DigniteAbpFieldCustomizingModule))]
    public class MyModule : AbpModule
    {
        //...
    }
}
````

#### 配置设置项目的表单

`Dignite.Abp.Settings`为`SettingDefinition`添加了一些扩展方法用于设置项目的表单。

`SetForm`方法用于设置项目的表单，同时还可以为表单定义分组，示例代码如下：

````csharp
using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing.TextboxForm;
using Dignite.Abp.FieldCustomizing.CheckboxForm;

namespace MyCompany.MyProject
{
    public class EmailSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingNavigation("General",L("General")),
                new Volo.Abp.Settings.SettingDefinition("Smtp.Host", "127.0.0.1")
                    .SetForm(form =>
                        form.UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "Smtp host";
                            tb.CharLimit=15;
                        }
                    ),L("Email")),                
                new Volo.Abp.Settings.SettingDefinition("Smtp.Port", "25")
                    .SetForm(form =>
                        form.UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "Smtp port";
                            tb.CharLimit=8;
                        }
                    ),L("Email")), 
                new Volo.Abp.Settings.SettingDefinition("Smtp.UserName")
                    .SetForm(form =>
                        form.UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "Smtp username";
                            tb.CharLimit=128;
                        }
                    ),L("Email")), 
                new Volo.Abp.Settings.SettingDefinition("Smtp.Password", isEncrypted: true)
                    .SetForm(form =>
                        form.UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "Smtp password";
                            tb.CharLimit=128;
                        }
                    ),L("Email")), 
                new Volo.Abp.Settings.SettingDefinition("Smtp.EnableSsl", "false")
                    .SetForm(form =>
                        form.UseCheckbox(cb =>
                        {
                            cb.Checked = false;
                        }
                    ),L("Email")), 
            ) ;
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MyProjectResource>(name);
        }
    }
}
````

## ISettingDefinitionManager 接口

`Dignite.Abp.Settings.ISettingDefinitionManager` 继承自`Volo.Abp.Settings.ISettingDefinitionManager`，除具有继承来的方法外，增加了如下几个方法：

- **GetNavigations()**：用于获取设置的所有导航，返回`SettingNavigation`列表集合
- **GetNavigation(string name)**：用于获取指定名称的导航，返回单个`SettingNavigation`对象
- **GetNavigationOrNull(string name)**：用于获取可为 `null` 的导航，

示例用法：

````csharp
using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing.TextboxForm;

public class MyService
{
    private readonly ISettingDefinitionManager _settingDefinitionManager;
    private readonly IFormProviderSelector _formProviderSelector;

    //Inject ISettingDefinitionManager in the constructor
    public MyService(
        ISettingDefinitionManager settingDefinitionManager,
        IFormProviderSelector formProviderSelector)
    {
        _settingDefinitionManager = settingDefinitionManager;
        _formProviderSelector = formProviderSelector;
    }
    
    public IList<SettingNavigation> GetNavigations()
    {
        var navigations = _settingDefinitionManager.GetNavigations();
        return navigations;
    }

    public IReadOnlyList<SettingDefinition> GetSettingDefinitions(string navigationName)
    {
        var navigation = _settingDefinitionManager.GetNavigationOrNull(navigationName);
        return navigation.SettingDefinitions;
    }

    public TextboxFormConfiguration SettingDefinitionForm(string navigationName,string settingName)
    {
        var navigation = _settingDefinitionManager.GetNavigation(navigationName);
        var setting1 = navigation.SettingDefinitions.Single(sf => sf.Name == settingName);
        var formConfig = setting1.GetFormOrNull();
        var formProvider = _formProviderSelector.Get(TextboxFormProvider.ProviderName);
        var textboxFormConfig = (TextboxFormConfiguration)formProvider.GetConfiguration(formConfig);
        return textboxFormConfig;
    }
}
````
