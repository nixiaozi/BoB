[中文](Document/zh_cn/Main.md)|[English](Document/en_us/Main.md)

# 介绍
BoB是Basic of Basic 的缩写。写这个框架最初是基于以下考虑：如何保持项目的结构清晰（尤其是项目在慢慢做大的过程中），以及如何在减少冗余代码的情况下力求简洁并保证项目代码的可读性？比如在开发的过程中经常不知道该如何进行封装导致写了大量的冗余代码，或是一个新功能不知道放在哪里合适，最后随意的放置弄乱了整个项目代码的结构。
这个项目主要使用模块拆分和分层来解决这个问题，可以阅读项目代码结构了解详细内容。同时这个框架也集成了很多经常会使用的功能如日志、缓存、配置修改等功能。
BoB基于.net Core平台可以轻松的实现跨平台部署。



# 功能和优势
1. 使用模块化注入,减少繁琐的依赖注入服务添加
    ```C#
    if (isAwesome){
      return true
    }
    ```
    
2. 灵活的自定义配置管理


3. 灵活的异常处理和日志记录功能


4. 可扩展的缓存处理功能


5. 简单的语言切换


6. 更多的功能来自你的参与


# 实例


:kissing_heart:更多实例开发中……敬请期待




# 代码库依赖
* .netCore v3.0
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


# 许可证
BoB项目使用了[MIT](LICENSE.txt)许可
