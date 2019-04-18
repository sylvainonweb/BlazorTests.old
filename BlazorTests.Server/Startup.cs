using BlazorTests.Common.Technical.Database;
using BlazorTests.Common.Technical.Database.DatabaseFunction;
using BlazorTests.Server.Entities.DatabaseSpecific;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using SD.LLBLGen.Pro.DQE.SqlServer;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;

namespace BlazorTests.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // AJOUT
            services.RegisterIocObjects();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Program>();

            // AJOUT
            app.UseLLBLGen();
        }
    }

    public static class ObjectContainer
    {
        public static IServiceCollection RegisterIocObjects(this IServiceCollection services)
        {
            // Action filters (Page Filters à venir)

            // Base de données
            services.AddScoped<FunctionMappingStore, SqlServerDatabaseFunctionMappingStore>();
            services.AddScoped<IDataAccessAdapterEx, DataAccessAdapter>();
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }

    public static class LLBLGenExtensions
    {
        public static IApplicationBuilder UseLLBLGen(this IApplicationBuilder app)
        {
            // ... this code is placed in a method called at application startup
            RuntimeConfiguration.AddConnectionString(
                "ConnectionString.SQL Server (SqlClient)",
                GetConnectionString());

            // Configure the DQE
            RuntimeConfiguration.ConfigureDQE<SQLServerDQEConfiguration>(c => c.SetTraceLevel(TraceLevel.Verbose)
                .AddDbProviderFactory(typeof(System.Data.SqlClient.SqlClientFactory))
                .SetDefaultCompatibilityLevel(SqlServerCompatibilityLevel.SqlServer2012));

            return app;
        }

        private static string GetConnectionString()
        {
            string datasource= "SQLEXPRESS_2016";
            if (Environment.MachineName == "DESKTOP-91MFTP0")
            {
                datasource = "SQLEXPRESS_2017";
            }

            return $"data source=.\\{datasource};initial catalog=WebTestsDatabase;User ID=WebTestsDatabaseUser;Password=WebTestsDatabaseUser;persist security info=False;packet size=4096";
        }
    }
}
