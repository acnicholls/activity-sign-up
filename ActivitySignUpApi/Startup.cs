using ActivitySignUp.Repositories;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation;
using ActivitySignUp.Validation.Interfaces;
using Dapper.AmbientContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dapper.AmbientContext.Storage;
using System.Reflection;
using System.IO;
using System;

namespace ActivitySignUpApi
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
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Activity Sign Up API v1");
                }
            );

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
