# *BoB(Basic of Basic)*
## 介绍
BoB是Basic of Basic 的缩写。写这个框架是为了找到一种方法持续保持项目的结构清晰（尤其是项目在慢慢做大的过程中），以及如何在减少冗余代码的情况下力求简洁并保证项目代码的可读性？比如在开发的过程中经常不知道该如何进行封装导致写了大量的冗余代码，或是一个新功能不知道放在哪里合适，最后随意的放置弄乱了整个项目代码的结构。
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
1、 第一种方法：直接下载这个项目，其中[src文件夹](src/)中就是项目内容。打开BoB.sln可以看到项目结构如下：

![vs structure](https://user-images.githubusercontent.com/13010868/77848256-15366000-71f6-11ea-858a-67ab529cbf79.png)

2、 第二种方法：可以使用命令行从模板安装项目
* 首先你需要新建一个文件夹BoBProject(这个名字可以取你想要的)用来存放你的项目。
* 然后打开powershell并且导航到BoBProject文件夹下，执行以下命令来下载需要的项目模板：
    ``` .NetCLI
    dotnet new --install BoB.BaseTemplate::1.0.3
    ```
    会得到类似结果：
    
    ![download template](https://user-images.githubusercontent.com/13010868/77848252-136c9c80-71f6-11ea-8929-58808c83a35d.png)
    
* 下一步在powershell中执行以下命令来添加所需项目：
    ``` .NetCLI
    dotnet new BoB.Api --output "BoB.Api"
    dotnet new BoB.AutoMapper --output "BoB.AutoMapperManager"
    dotnet new BoB.BaseConfig --output "BoB.BaseConfiguration"
    dotnet new BoB.BaseModule --output "BoB.BaseModule"
    dotnet new BoB.Exceptions --output "BoB.BoBExceptions"
    dotnet new BoB.BoBLogger --output "BoB.BoBLogger"
    dotnet new BoB.CacheManager --output "BoB.CacheManager"
    dotnet new BoB.ContainManager --output "BoB.ContainManager"
    dotnet new BoB.EFDbContext --output "BoB.EFDbContext"
    dotnet new BoB.ExtendAndHelper --output "BoB.ExtendAndHelper"
    dotnet new BoB.LanguageManager --output "BoB.LanguageManager"
    dotnet new BoB.UseBus --output "BoB.UseBus"
    
    ```
    会得到如下输出：
    
    ![add project](https://user-images.githubusercontent.com/13010868/77848249-123b6f80-71f6-11ea-91e7-69b2f90b98dc.png)
    
* 然后添加sln文件生成解决方案，在powershell中运行：
    ``` .NetCLI
    dotnet new sln -n BoB
    ```
    会得到如下输出：
    
    ![add sln](https://user-images.githubusercontent.com/13010868/77848251-136c9c80-71f6-11ea-87a9-4cb2551ac835.png)
    
    解决方案名称为BoB
* 最后在powershell中执行以下命令，把前面生成的项目添加入解决方案
    ``` .NetCLI
    dotnet sln BoB.sln add BoB.Api/BoB.Api.csproj BoB.AutoMapperManager/BoB.AutoMapperManager.csproj BoB.BaseConfiguration/BoB.BoBConfiguration.csproj BoB.BaseModule/BoB.BaseModule.csproj BoB.BoBExceptions/BoB.BoBExceptions.csproj BoB.BoBLogger/BoB.BoBLogger.csproj BoB.CacheManager/BoB.CacheManager.csproj BoB.ContainManager/BoB.ContainManager.csproj BoB.EFDbContext/BoB.EFDbContext.csproj BoB.ExtendAndHelper/BoB.ExtendAndHelper.csproj BoB.LanguageManager/BoB.LanguageManager.csproj BoB.UseBus/BoB.UseBus.csproj
    ```
    会得到如下输出
    
    ![project in sln](https://user-images.githubusercontent.com/13010868/77848253-14053300-71f6-11ea-99b0-7d0d71092596.png)
    
    现在一个基础的后台框架已经完成了，你现在可以打开BoB.sln来查看解决方案，可以在这个框架的基础上编写自己的业务代码。同时为了让我们有个更加直观的了解，我特意做了一个简单的实例，下面是加载示例模板的代码：
    * 首先在powershell中执行以下命令来添加所需示例项目：
    ``` .NetCLI
    dotnet new BoB.HelloWorldApi --output "BoB.HelloWorldApi"
    dotnet new BoB.MainDatabase --output "BoB.MainDatabase"
    dotnet new BoB.PeopleEntities --output "BoB.PeopleEntities"
    dotnet new BoB.WorldAction --output "BoB.WorldAction"
    ```
    * 然后把这几个项目添加到BoB.sln这个解决方案，可以通过执行以下命令来实现：
    ``` .NetCLI
    dotnet sln BoB.sln add BoB.HelloWorldApi/BoB.HelloWorldApi.csproj BoB.MainDatabase/BoB.MainDatabase.csproj BoB.PeopleEntities/BoB.PeopleEntities.csproj BoB.WorldAction/BoB.WorldAction.csproj
    ```
    
    **备注：由于.netCli 命令行工具无法创建解决方案文件夹类似的关联，所以项目没有分层结构。需要在visual studio 中手动修改**
    打开浏览器访问链接 https://localhost:5001/swagger/index.html 即可看到效果：
    
    ![quick result](https://user-images.githubusercontent.com/13010868/77848255-149dc980-71f6-11ea-85c4-e1a4ae9b3eb5.png)
    
    **备注：.netcore项目可以通过命令行直接运行：**
    首先需要在BoBProject文件夹下新建一个文本文件，在文本文件中添加如下脚本
    ``` .NetCLI
    call SET SolutionDir="%cd%"
    call SET RunProjectDir="%cd%\BoB.HelloWorldApi"
    call dotnet run --project  "%RunProjectDir%"
    ```
    然后重命名该文本文件为start.cmd。最后确保powershell在BoBProject目录下运行，运行下面脚本
    ``` .NetCLI
    .\start.cmd
    ```
    会得到如下输出
    
    打开浏览器访问链接 https://localhost:5001/swagger/index.html 即与上面同样的效果。

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

## 联系作者
邮箱：nixiaozi01@hotmail.com
QQ: 776493846


## 许可证
BoB项目使用了[MIT](LICENSE.txt)许可
