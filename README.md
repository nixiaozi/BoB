# *BoB(Basic of Basic)*
## 介绍
BoB是Basic of Basic 的缩写。写这个框架最初是基于以下考虑：如何保持项目的结构清晰（尤其是项目在慢慢做大的过程中），以及如何在减少冗余代码的情况下力求简洁并保证项目代码的可读性？比如在开发的过程中经常不知道该如何进行封装导致写了大量的冗余代码，或是一个新功能不知道放在哪里合适，最后随意的放置弄乱了整个项目代码的结构。
这个项目主要使用模块拆分和分层来解决这个问题，可以阅读项目代码结构了解详细内容。同时这个框架也集成了很多经常会使用的功能如日志、缓存、配置修改等功能。
BoB基于.net Core平台可以轻松的实现跨平台部署。



## 功能和优势
* 使用模块化注入,减少繁琐的依赖注入服务添加
    一般依赖注入都是使用单个服务进行注入例如：
    ```C#
    builder.RegisterType<MemberService>().As<IMemberService>().InstancePerLifetimeScope();
    builder.RegisterType<OpenAccountOrderService>().As<IOpenAccountOrderService>().InstancePerLifetimeScope();
    builder.RegisterType<ActionLogService>().As<IActionLogService>().InstancePerLifetimeScope();
    ***
    ```
    现在使用模块注入单个模块内的服务只需要注入一次就可以达到同样的效果
    ```C#
    builder.RegisterModule<AccountLogModule>();
    ```
    
* 灵活的自定义配置管理
    可以在每个模块中轻松的添加配置
    ```C#
    [WriteAble]
    public static readonly TestPeople testPeople = new TestPeople { Age = 45, Name = "我嫩的都是", HasPen=false,Now=new DateTime(1024,8,5,6,47,52) };
    [WriteAble]
    public static readonly int TestInt=67;
    [WriteAble]
    public static readonly bool TestBool = false;
    ```
    项目运行时会自动生成一个BoBConfig.json文件来存储各个模块的所有课编辑的配置，你可以通过修改BoBConfig.json文件的配置值来修改配置。
    

* 灵活的异常处理和日志记录功能
    集成了Serilog日志模块，你可以定制模板。当然你还可以使用你自己喜欢的。
    ```C#
    var _log = BoBContainer.ServiceProvider.GetService<IBoBLogService>();
    _log.Verbose(Message, _ModuleLay, _ModuleName, _MethodName, HResult, exception);
    ```

* 可扩展的缓存处理功能
   使用缓存是提高系统性能的重要方式，你可以自由使用自己喜欢的缓存控制如redis。
   ```C#
    var LangDics = _cacheService.Get<Dictionary<string, string>>(CacheTag.BoBLangService, theLangType.ToString(),
        () =>
        {
            // return to cache data
        },3600);
    ```

* 简单的语言切换
    使用不同的语种使用不同的json文件，来提供对语种翻译的支持。
    ```C#
    public ILangService _langService;
    ***
    _langService.L("CustomEnvironmentLeo")
    ```


更多的功能来自你的参与:kissing_heart:

## 开始使用
* 首先你需要新建一个文件夹BoBProject(这个名字可以取你想要的)用来存放你的项目，然后进入这个文件夹并新建四个子文件夹 BoB.Base、BoB.Core、BoB.Module 和 BoB.Use。最后进入 BoB.Module 文件夹并新建两个子文件夹 Actions 和 OTO。最后文件结构如下图：


* 然后打开powershell并且导航到BoBProject文件夹下，执行以下命令以获取项目模板：
    ``` .NetCLI
    dotnet new --install BoB.BaseTemplate::1.0.3
    ```
* 然后


## 实例


:kissing_heart:更多实例开发中……敬请期待




## 代码库依赖
* .netCore v3.1
* Autofac v4.9.4
* Autofac.Configuration v4.1.0
* Autofac.Extensions.DependencyInjection v5.0.1
* AutofacSerilogIntegration v2.1.0
* AutoMapper v9.0.0
* Microsoft.EntityFrameworkCore v3.1.2
* Microsoft.Extensions.Caching.Memory v3.1.3
* Newtonsoft.Json v12.0.3
* Serilog.Sinks.Debug v1.0.1
* Serilog.Sinks.File v4.1.0
* Swashbuckle.AspNetCore v5.2.1
* System.Runtime v4.3.1


## 许可证
BoB项目使用了[MIT](LICENSE.txt)许可
