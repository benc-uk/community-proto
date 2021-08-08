using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CommunityApi.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace CommunityApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Set up CosmosDB service
        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection, ILogger<ICosmosDbService> logger)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string commContainerName = configurationSection.GetSection("CommunityContainerName").Value;
            string userContainerName = configurationSection.GetSection("UserContainerName").Value;
            string discussionContainerName = configurationSection.GetSection("DiscussionContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            CosmosClientOptions clientOptions = new CosmosClientOptions()
            {
                // SerializerOptions = new CosmosSerializationOptions()
                // {
                //     IgnoreNullValues = true
                // }
            };
            CosmosClient client = new CosmosClient(account, key, clientOptions);
            CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, commContainerName, userContainerName, discussionContainerName, logger);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(commContainerName, "/id");
            await database.Database.CreateContainerIfNotExistsAsync(userContainerName, "/id");
            await database.Database.CreateContainerIfNotExistsAsync(discussionContainerName, "/id");
            //await database.Database.DefineContainer(userContainerName, "/id").WithUniqueKey().Path("/uid").Attach().CreateIfNotExistsAsync();

            return cosmosDbService;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Community API", Version = "v1" });
            });
            services.AddSingleton<ICosmosDbService>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<ICosmosDbService>>();
                return InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb"), logger).GetAwaiter().GetResult();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Community API v1"));
            }

            app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseAuthorization();

            // Useful when testing
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // To serve the static JS frontend SPA
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


}
