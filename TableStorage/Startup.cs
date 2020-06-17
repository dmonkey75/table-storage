using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grundfos.GiC.Shared.Azure.Storage;
using Grundfos.GiC.Shared.Defense;
using Grundfos.GiC.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TableStorage.Models;
using TableStorage.Stores;

namespace TableStorage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddReadModelRepositories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public void AddReadModelRepositories(IServiceCollection services)
        {
            var azureStorageConnection = GetSetting("Values:AzureStorageConnection");
            var policyServerStore = new PolicyServerStore(azureStorageConnection); 

            services.AddSingleton<ITableStorageRepository<PolicyServerMap>>(policyServerStore); 
        }

        private Result<string> GetSetting(string key)
        {
            var configSetting = Configuration.GetSection(key);
            if (configSetting.Value.IsNotNullOrEmpty())
                return Result.Ok(configSetting.Value);

            var environmentSetting = Environment.GetEnvironmentVariable(key);
            if (environmentSetting.IsNotNullOrEmpty())
                return Result.Ok(environmentSetting);

            return Result.Fail<string>($"Could not locate the key: {key}");
        }
    }
}