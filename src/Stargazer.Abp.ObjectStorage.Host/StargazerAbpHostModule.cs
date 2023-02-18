using System;
using System.Linq;
using Lemon.Common.Extend;
using Stargazer.Abp.ObjectStorage.Application;
using Stargazer.Abp.ObjectStorage.EntityFrameworkCore;
using Stargazer.Abp.ObjectStorage.HttpApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.ObjectStorage.Host
{
    [DependsOn(
    typeof(StargazerAbpObjectStorageEntityFrameworkCoreModule),
    typeof(StargazerAbpObjectStorageApplicationModule),
    typeof(StargazerAbpObjectStorageHttpApiModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule)
    )]
    public class StargazerAbpObjectStorageHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stargazer.Abp.ObjectStorage Service API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }

        // private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        // {
        //     context.Services.AddAuthentication("Bearer")
        //         .AddIdentityServerAuthentication(options =>
        //         {
        //             options.Authority = configuration["AuthServer:Authority"];
        //             options.ApiName = configuration["AuthServer:ApiName"];
        //             options.RequireHttpsMetadata = false;
        //         });
        // }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            context.Services.AddMvcCore().AddNewtonsoftJson(
                op =>
                {
                    op.SerializerSettings.ContractResolver =
                        new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    op.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    op.SerializerSettings.Converters.Add(new Ext.DateTimeJsonConverter());
                    op.SerializerSettings.Converters.Add(new Ext.LongJsonConverter());
                });
            
            // Configure<AbpAspNetCoreMvcOptions>(options =>
            // {
            //     options.ConventionalControllers.Create(
            //         typeof(StargazerAbpObjectStorageApplicationModule).Assembly,
            //         setting =>
            //         {
            //             setting.RootPath = "oos";
            //         });
            // });
            
            // ConfigureAuthentication(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureCors(context, configuration);
        }

        public override void OnApplicationInitialization(
            ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors(DefaultCorsPolicyName);

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseConfiguredEndpoints();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stargazer.Abp.ObjectStorage Service API");
            });

        }
    }
}

