using ActivitySignUp.Repositories;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation;
using ActivitySignUp.Validation.Interfaces;
using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ActivitySignUp.Api
{
    /// <summary>
    ///  the startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// startup ctor
        /// </summary>
        /// <param name="configuration">the application configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// the application configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        private const string _myCorsPolicyName = "AllowFromFrontEnd";

        /// <summary>
        /// this method configures the services container for dependency injection
        /// </summary>
        /// <param name="services">the services container from the system's injection process</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                var clientUrl = Configuration.GetSection("ClientUrl").Value;
                options.AddPolicy(name: _myCorsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins(clientUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            services.AddControllers();

            services.AddSingleton<IConfiguration>(Configuration);

            AmbientDbContextStorageProvider.SetStorage(new AsyncLocalContextStorage());

            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            services.AddTransient<IAmbientDbContextFactory, AmbientDbContextFactory>();
            services.AddTransient<IAmbientDbContextLocator, AmbientDbContextLocator>();

            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IPersonService, PersonService>();

            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            services.AddTransient<IActivityValidators, ActivityValidators>();
            services.AddTransient<ICommentValidators, CommentValidators>();
            services.AddTransient<IPersonValidators, PersonValidators>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ActivitySignUpApi", Version = "v1" });
                //c.OperationFilter<ExamplesOperationFilter>();
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        /// <summary>
        /// this method will configure the pipeline
        /// </summary>
        /// <param name="app">the application to configure</param>
        /// <param name="env">the environment to configure the application for</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Activity Sign Up API v1");
                    c.RoutePrefix = string.Empty;
                }
            );

            app.UseRouting();

            app.UseCors(_myCorsPolicyName);

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
