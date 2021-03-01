using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Application;
using BlogCore.Domain;
using BlogCore.Extended;
using BlogCore.Repository;
using Castle.Core.Logging;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Text;

namespace BlogCore
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
            ConfigManagerConf.SetConfiguration(Configuration);
        }

        public IConfigurationRoot Configuration { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // 运行时将调用此方法。使用此方法将服务添加到容器。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers()
                .AddControllersAsServices(); //属性注入必须加上这个

            //注入EF Core数据库上下文服务
            services.AddDbContext<DefaultContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("ConnectionStrings:Default")));

            #region Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "框架说明文档",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "Blog.Core", Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "BlogCore.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath,true);
                #region Token绑定到ConfigureServices
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "JWT授权",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
            #endregion

            #region 授权机制
            // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());//单独角色
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//或的关系
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//且的关系
            });
            #endregion

            #region 认证3.1
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            services.AddAuthentication(x =>
            {
                //看这个单词熟悉么？没错，就是上边错误里的那个。
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })// 也可以直接写字符串，AddAuthentication("Bearer")
              .AddJwtBearer(o =>
              {
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      NameClaimType = JwtClaimTypes.Name,
                      RoleClaimType = JwtClaimTypes.Role,
                      ValidIssuer = "http://localhost:5200",
                      ValidAudience = "api",
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppConsts.Secret)),

                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                      RequireExpirationTime = true,// 是否要求Token的Claims中必须包含Expires
                  };

              });
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            #endregion

            //禁用端点路由
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // 您可以在ConfigureContainer中直接注册内容
        //与Autofac。此操作在ConfigureServices之后运行，因此此处的内容将覆盖ConfigureServices中进行的注册。
        // 不要建造容器；由工厂为您完成。
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在此处直接向Autofac注册您自己的东西。不要调用为您在AutofacServiceProviderFactory中发生的builder.Populate（）。
            builder.RegisterModule(new ServiceModule());
        }

        // 运行时将调用此方法。使用此方法来配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(p => {
                    p.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                    //如果想直接在域名的根目录直接加载 swagger 比如访问：localhost:8001 就能访问，可以这样设置：
                    //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
                    p.RoutePrefix = "";
                });
                #endregion
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            // 跳转https
            app.UseHttpsRedirection();
            app.UseRouting();
            //先开启认证
            app.UseAuthentication();
            //开启授权中间件
            app.UseAuthorization();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            app.UseMvc();
        }
    }
}
