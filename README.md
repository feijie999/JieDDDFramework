# JieDDDFramework
 基于EntityFrameworkCore2.2实现的领域驱动框架，框架平台采用.NetCore2.2 

 适合中小型Web项目的开发，需要处理非常高并发的项目请采用[Ray](https://github.com/feijie999/Ray)框架

## 框架采用了以下技术：

* AspNetCore2.2
* Entity Framework Core2.2
  * 非侵入式的实体模型配置注入
  * 软删除支持
  * 实现`DomanDbContext`已更好的支持DDD
* DDD领域驱动设计
  * (Entities/AggregateRoot)实体与领域根
  * Repositories 仓储模式，已实现Entity Framework。Example中使用了Mysql为数据库
  * Domain Event 基于MediatR实现
  * Unit Of Wrok工作单元模式，基于数据库实现的事务
  * 充血模式
* 模块化开发与微服务架构
* 依赖注入
* 非侵入式的数据有效验证与统一的错误信息返回，包含错误码
* 统一的异常拦截处理，业务领域层只需抛出异常，无需处理
* 日志记录
* EventBus 事件总线 (功能添加中)
* 身份验证与授权管理,通过`IdentityServer4`实现

## Example

 **实现了两个模块:**

* IdentityServer（Identity.API）认证服务中心
* OrderServer (Order.API) 订单服务

**环境**

* dotnetcore 2.2
* mysql 如使用其他关系型数据库需更改startup中如下代码已适配数据迁移功能
```csharp
 void OptionActions(DbContextOptionsBuilder option)
            {
                option.UseMySql(settings.ConnectionString,
                    sqlOptions => { sqlOptions.MigrationsAssembly(assemblyName); });
            }

```

**启动**

1. 配置appsettings.json中的数据库连接字符串
2. 启动Identity.API（http://localhost:5000/swagger/index.html）
3. 启动Order.API（http://localhost:8990/swagger/index.html）
4. 调用Order.API中的Login接口,参数{
    "email": "demouser@xx.com",
    "password": "123456"
}并得到accessToken
5. 在swagger中使用accessToken
 ![image](https://github.com/feijie999/JieDDDFramework/raw/master/docs/pic1.png)
6. 调用相关接口
