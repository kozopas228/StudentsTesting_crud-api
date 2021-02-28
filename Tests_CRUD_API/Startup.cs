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
using Tests_CRUD_BLL.Services.Implementation;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.Util.Mappers.Implementation;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
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
            AddDb(services);
            ConfigureDependencies(services);
        }

        public virtual void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<ITestThemeRepository, TestThemeRepository>();
            services.AddTransient<IAnswerMapper, AnswerMapper>();
            services.AddTransient<IQuestionMapper, QuestionMapper>();
            services.AddTransient<ITestMapper, TestMapper>();
            services.AddTransient<ITestThemeMapper, TestThemeMapper>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ITestThemeService, TestThemeService>();
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
