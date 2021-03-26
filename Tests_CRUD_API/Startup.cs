using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Own_IoC_Container;
using Tests_CRUD_BLL.Services.Implementation;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.Util.Mappers.Implementation;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL;
using Tests_CRUD_DAL.Repositories.Implementation;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => 
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            AddDb(services);
            ConfigureDependencies(services);
        }

        public virtual void ConfigureDependencies(IServiceCollection services)
        {
            var servs = new DiServiceCollection();

            servs.RegisterTransient<IAnswerRepository, AnswerRepository>();
            servs.RegisterTransient<IQuestionRepository, QuestionRepository>();
            servs.RegisterTransient<ITestRepository, TestRepository>();
            servs.RegisterTransient<ITestThemeRepository, TestThemeRepository>();
            servs.RegisterTransient<IAnswerMapper, AnswerMapper>();
            servs.RegisterTransient<IQuestionMapper, QuestionMapper>();
            servs.RegisterTransient<ITestMapper, TestMapper>();
            servs.RegisterTransient<ITestThemeMapper, TestThemeMapper>();
            servs.RegisterTransient<IAnswerService, AnswerService>();
            servs.RegisterTransient<IQuestionService, QuestionService>();
            servs.RegisterTransient<ITestService, TestService>();
            servs.RegisterTransient<ITestThemeService, TestThemeService>();

            servs.RegisterSingleton(services.BuildServiceProvider().GetService<ApplicationContext>());

            var container = servs.GenerateContainer();

            services.AddSingleton<DiContainer>(container);
        }

        private void AddDb(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<Tests_CRUD_DAL.ApplicationContext>(options =>
                    options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                services.AddDbContext<Tests_CRUD_DAL.ApplicationContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
