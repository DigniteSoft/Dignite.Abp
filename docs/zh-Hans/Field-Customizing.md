# Field Customizing

## 介绍

`Dignite.Abp.FieldCustomizing`是为Abp开发的基础模块，，主要作用有两个：

1. 表单引擎
    `Dignite.Abp.FieldCustomizing`提供了文本输入框、下拉菜单、单选、复选、富文体编辑器、日期选择、颜色选择等简单类型表单，以及表格、矩阵等复杂类型表单。
    此外，任何人都可以基于几个接口创造更多的表单。详见[创造字段表单](#创造字段表单)

2. 字段引擎
    `Dignite.Abp.FieldCustomizing`提供了*字段引擎*，允许客户根据业务需要创建业务表单，动态的为业务逻辑添加字段，配合表单引擎，满足机动、多变的业务需求。
    `Dignite.Abp.FieldCustomizing`与`[Volo.Abp.ObjectExtending](https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions)`的区别：
    - `Volo.Abp.ObjectExtending`是在程序编译之前，由软件开发人员以编程方式扩展对象额外属性；
    - `Dignite.Abp.FieldCustomizing`自定义字段引擎是在程序运行期间，由用户扩展对象的额外属性；

## 安装

[Dignite.Abp.FieldCustomizing](https://www.nuget.org/packages/Dignite.Abp.FieldCustomizing)是定义Field Customizing服务的主包.

在项目中安装 `Dignite.Abp.FieldCustomizing`NuGet包，然后添加`[DependsOn(typeof(DigniteAbpFieldCustomizingModule))]`模块依赖。

## 表单引擎

### 创建表单配置

继承`FormConfigurationBase`抽象类构建表单的配置项目，该抽象类具有以下两个属性：

- Required：配置表单是否必填；

- Description：配置表单的说明文本；

> 以上两个属性是表单的通用属性，因此新建的表单不需要重写。

下面写一个简单文本框表单的配置：

````csharp
public class SimpleTextboxFormConfiguration:FormConfigurationBase
    {
        public string Placeholder
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<string>("Placeholder", null);
            set => _fieldFormConfiguration.SetConfiguration("Placeholder", value);
        }

        public int CharLimit
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault("CharLimit", 64);
            set => _fieldFormConfiguration.SetConfiguration("CharLimit", value);
        }

        public TextboxFormConfiguration(FormConfigurationData fieldConfiguration)
            :base(fieldConfiguration, SimpleTextboxFormProvider.ProviderName)
        {
        }
    }
````

### 创建表单

继承`FormProviderBase`抽象类创造字段表单，需要重写实现的属性及方法如下：

#### Name

业务模块中的唯一名称，一般使用字母或字母+数字方式命名，注意区分大小写。

#### DisplayName

本地化的字符串，用于在UI上显示名称。

#### FormType

FormType分为FormType.Simple类型和FormType.Complex类型。
表格、矩阵是FormType.Complex类型。

#### GetConfiguration方法

用于获取本表单的配置。

#### Validate方法

用于验证提交数据。

下面写一个简单文本框表单的demo：

````csharp
public class SimpleTextboxFormProvider : FormProviderBase
    {
        public const string ProviderName = "SimpleTextboxForm";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.SimpleTextboxForm"];

        public override FormType FormType => FormType.Simple;

        public override void Validate(FormValidateArgs args)
        {
            var configuration = new SimpleTextboxFormConfiguration(args.FieldDefinition.FormConfiguration);

            if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:Required"].Value, 
                        new[] { args.FieldDefinition.Name }
                        ));
            }

            if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:CharacterCountExceedsLimit",args.FieldDefinition.DisplayName, configuration.CharLimit],
                        new[] { args.FieldDefinition.Name }
                        ));
            }
        }

        public override FormConfigurationBase GetConfiguration(FormConfigurationData fieldConfiguration)
        {
            return new SimpleTextboxFormConfiguration(fieldConfiguration);
        }
````

如上，创造了一个新表单，`Dignite.Abp.FieldCustomizing`会自动发现并添加它，在你的程序中添加如下代码即可获取所有表单集合（虽然一般情况不需要这么做）：

````csharp
public class DemoAppService : ApplicationService
{
    protected IEnumerable<IFormProvider> FormProviders { get; }
    public DemoAppService(IEnumerable<IFormProvider> _formProviders)
    {
        FormProviders=_formProviders;
    }
}
````

### 表单数据验证

将Dto实现`CustomizableObject`基类进行表单数据验证，该基于有一个返回`BasicCustomizeFieldDefinition`对象集合的`GetFieldDefinitions`方法，需要在Dto中重写实现。

> `BasicCustomizeFieldDefinition` 包含了字段及表单的定义，是一个普通的c#类，可由任何的自定义字段源初始化。
下面是`Dignite.SiteBuilding`项目中`EntryEditDto`的代码片段，

````csharp
public class EntryEditDto: CustomizableObject
    {
        public EntryEditDto()
        {
            this.CustomizedFields = new CustomizeFieldDictionary();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid EntryTypeId { get; set; }

        [Required]
        public DateTime PublishTime { get; set; }

        ///<summary>
        /// Get the definition information of the field for data validation
        ///</summary>
        public override IReadOnlyList<BasicCustomizeFieldDefinition> GetFieldDefinitions(ValidationContext validationContext)
        {
            var _entryTypeAppService = validationContext.GetRequiredService<IEntryTypeAppService>();
            var entryType = _entryTypeAppService.GetAsync(EntryTypeId).Result;
            return entryType.FieldDefinitions
                .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName,
                        fd.DefaultValue,
                        fd.FormConfiguration
                        )).ToList(); 

        }
    }
````

## 字段引擎

### IHasCustomizableFields 接口

这是一个含有自定义表单集合的接口。它定义了 Dictionary 属性:

````csharp
CustomizeFieldDictionary CustomizedFields { get; }
````

#### 基本扩展方法

虽然可以直接使用类的 CustomizedFields 属性,但建议使用以下扩展方法使用额外属性：

#### SetField

用于设置自定义字段的值:

````csharp
entry.SetField("title", "My Title");
entry.SetField("content", "<p>Text content</p>");
````

#### GetProperty

用于读取自定义字段的值:

````csharp
var title = entry.GetField<string>("title");
var content = entry.GetField<string>("content");
````

- `GetField` 是一个泛型方法，对象类型做为泛型参数.
- 如果未设置给定的属性,则返回默认值 (int 的默认值为 0 , bool 的默认值是 false ... 等).

#### 非基本属性类型

如果你的属性类型不是原始类型(int,bool,枚举,字符串等),你需要使用 GetField 的非泛型版本,它会返回 object.

#### HasField

用于检查对象是否含有指定的字段。

#### RemoveField

用于从对象中删除自定义的字段，使用此方法代替为属性设置 null 值。

#### SetDefaultsForCustomizeFields

当某些自定义字段值为null时，该扩展方法用于将值为null的字段的值设置为`BasicCustomizeFieldDefinition`中设定的`DefaultValue`。

#### SetCustomizeFieldsToRegularProperties

用于设定与自定义字段同名的对象属性值。
> 如果你使用的是[AutoMapper](https://automapper.org/)库，同时在映射配置文件中使用 MapCustomizeFields() 方法且参数`mapToRegularProperties`值设定为`true`，程序将自动调用对象的`SetCustomizeFieldsToRegularProperties`扩展方法。

### 对象到对象映射

#### MapCustomizeFieldsTo

`MapCustomizeFieldsTo` 是`IHasCustomizableFields`的扩展方法，用于以受控方式将自定义字段从一个对象复制到另一个对象. 示例:

````csharp
entry.MapCustomizeFieldsTo(entryDto);
````

`MapCustomizeFieldsTo` 需要在**两侧**(本例中是`Entry` 和 `EntryDto`)实现`IHasCustomizableFields`接口。

#### 指定映射自定义字段

默认情况下自动映射全部自定义字段，通过`MapCustomizeFieldsTo`中`fields`参数，可以映射指定的自定义字段，如下所示：

````csharp
entry.MapCustomizeFieldsTo(
    entryDto,
    fields: string[]{"FieldName1","FieldName2"}
);
````

以上代码示例`entry`对象仅向`entryDto`对象映射"FieldName1"、"FieldName1"两个自定义字段。

#### 忽略字段

默认情况下不会忽略任何自定义字段的映射，通过`MapCustomizeFieldsTo`中`ignoredFields`参数，可以在映射操作中忽略某些字段:

````csharp
identityUser.MapCustomizeFieldsTo(
    identityUserDto,
    ignoredFields: new[] {"FieldName2"}
);
````

忽略的自定义字段不会复制到目标对象。

#### AutoMapper集成

如果你使用的是[AutoMapper](https://automapper.org/)库，`Dignite.Abp.FieldCustomizing`还提供了一种扩展方法来利用上面定义的 `MapCustomizeFieldsTo` 方法.

你可以在映射配置文件中使用 `MapCustomizeFields()` 方法.

````csharp
public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<IdentityUser, IdentityUserDto>()
            .MapCustomizeFields();
    }
}
````

它与 `MapCustomizeFieldsTo()` 方法具有相同的参数.

### Configure Ef Core Entity

`Dignite.Abp.FieldCustomizing.EntityFrameworkCore`有一些实用的扩展方法来配置从`ICustomizeFieldDefinition`接口和`IHasCustomizableFields`接口继承的属性。

在EntityFrameworkCore项目中安装[Dignite.Abp.FieldCustomizing.EntityFrameworkCore](https://www.nuget.org/packages/Dignite.Abp.FieldCustomizing.EntityFrameworkCore)Nuget包，然后添加`[DependsOn(typeof(DigniteAbpFieldCustomizingEntityFrameworkCoreModule))]`模块依赖。

#### ICustomizeFieldDefinition

`ICustomizeFieldDefinition`定义字段实体类的接口。

示例: 假设有一个直接继承 `AggregateRoot<Guid>` 基类和`ICustomizeFieldDefinition`接口的 `BookFieldDefinition` 实体:

````csharp
public class BookFieldDefinition : AuditedAggregateRoot<Guid>,ICustomizeFieldDefinition
{
    public string Group { get; set; }
}
````

`ConfigureCustomizableFieldDefinitions()` 是配置字段定义实体基本属性和约定的方法。在你的 `DbContext` 重写 `OnModelCreating` 方法并且做以下配置:

````csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    //Always call the base method
    base.OnModelCreating(builder);

    builder.Entity<BookFieldDefinition>(b =>
    {
        //Configure table
        b.ToTable("BookFieldDefinitions");

        b.ConfigureByConvention();
        b.ConfigureCustomizableFieldDefinitions();

        //Properties
        b.Property(q => q.Group).HasMaxLength(64);
    });
}
````

#### IHasCustomizableFields接口

`IHasCustomizableFields`是含有自定义字段值实体类的接口。

延续上面的代码，新建继承 `AggregateRoot<Guid>` 基类和`IHasCustomizableFields`接口的 `Book` 实体:

````csharp
public class Book : AuditedAggregateRoot<Guid>,IHasCustomizableFields
{
    public string Name { get; set; }
}
````

`ConfigureObjectCustomizedFields()` 是配置含有自定义字段值实体基本属性和约定的方法。在你的 `DbContext` 重写 `OnModelCreating` 方法并且做以下配置:

````csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    //Always call the base method
    base.OnModelCreating(builder);    

    builder.Entity<BookFieldDefinition>(b =>
    {
        //Configure table
        b.ToTable("BookFieldDefinitions");

        b.ConfigureByConvention();
        b.ConfigureCustomizableFieldDefinitions();

        //Properties
        b.Property(q => q.Group).HasMaxLength(64);
    });

    builder.Entity<Book>(b =>
    {
        //Configure table
        b.ToTable("Books");

        b.ConfigureByConvention();
        b.ConfigureObjectCustomizedFields();

        //Properties
        b.Property(q => q.Name).HasMaxLength(128);
    });
}
````

## 应用案例

### Dignite.Abp.Settings

### Dignite.SiteBuilding
