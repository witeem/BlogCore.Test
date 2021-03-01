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

        // ����ʱ�����ô˷�����ʹ�ô˷�����������ӵ�������
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers()
                .AddControllersAsServices(); //����ע�����������

            //ע��EF Core���ݿ������ķ���
            services.AddDbContext<DefaultContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("ConnectionStrings:Default")));

            #region Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "���˵���ĵ�",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "Blog.Core", Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "BlogCore.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath,true);
                #region Token�󶨵�ConfigureServices
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "JWT��Ȩ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
            #endregion

            #region ��Ȩ����
            // 1����Ȩ����������ϱߵ�����ͬ�����ô����ǲ�����controller�У�д��� roles ��
            // Ȼ����ôд [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());//������ɫ
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//��Ĺ�ϵ
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//�ҵĹ�ϵ
            });
            #endregion

            #region ��֤3.1
            //��ȡ�����ļ�
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            services.AddAuthentication(x =>
            {
                //�����������Ϥô��û�������ϱߴ�������Ǹ���
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })// Ҳ����ֱ��д�ַ�����AddAuthentication("Bearer")
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
                      // �Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,//����ǻ������ʱ�䣬Ҳ����˵����ʹ���������˹���ʱ�䣬����ҲҪ���ǽ�ȥ������ʱ��+���壬Ĭ�Ϻ�����7���ӣ������ֱ������Ϊ0
                      RequireExpirationTime = true,// �Ƿ�Ҫ��Token��Claims�б������Expires
                  };

              });
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            #endregion

            //���ö˵�·��
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // ��������ConfigureContainer��ֱ��ע������
        //��Autofac���˲�����ConfigureServices֮�����У���˴˴������ݽ�����ConfigureServices�н��е�ע�ᡣ
        // ��Ҫ�����������ɹ���Ϊ����ɡ�
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // �ڴ˴�ֱ����Autofacע�����Լ��Ķ�������Ҫ����Ϊ����AutofacServiceProviderFactory�з�����builder.Populate������
            builder.RegisterModule(new ServiceModule());
        }

        // ����ʱ�����ô˷�����ʹ�ô˷���������HTTP����ܵ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(p => {
                    p.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                    //�����ֱ���������ĸ�Ŀ¼ֱ�Ӽ��� swagger ������ʣ�localhost:8001 ���ܷ��ʣ������������ã�
                    //���ʱ��ȥlaunchSettings.json�а�"launchUrl": "swagger/index.html"ȥ���� Ȼ��ֱ�ӷ���localhost:8001/index.html����
                    p.RoutePrefix = "";
                });
                #endregion
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            // ��תhttps
            app.UseHttpsRedirection();
            app.UseRouting();
            //�ȿ�����֤
            app.UseAuthentication();
            //������Ȩ�м��
            app.UseAuthorization();
            // ���ش�����
            app.UseStatusCodePages();//�Ѵ����뷵��ǰ̨��������404
            app.UseMvc();
        }
    }
}
