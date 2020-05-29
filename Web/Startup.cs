using Application;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Web.Services;

namespace Web
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
            services.AddDbContext<TrainingContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("StudentCertificationConnection")));

            var queriesConnectionString = new QueriesConnectionString(Configuration["QueriesConnectionString"]);
            services.AddSingleton(queriesConnectionString);

            services.AddMediatR(ApplicationAssembly());
            services.AddMediatR(EventAssembly());
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddHttpClient<IStudentService, StudentService>();
            services.AddHttpClient<ICertificationService, CertificationService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICertificationRepository, CertificationRepository>();
            services.AddScoped<IQueryRepository, QueryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }

        private static Assembly StartupAssembly()
        {
            return typeof(Startup).GetTypeInfo().Assembly;
        }

        private static Assembly ApplicationAssembly()
        {
            return typeof(GetStudentRecordsQuery).GetTypeInfo().Assembly;
        }

        private static Assembly EventAssembly()
        {
            return typeof(StudentObtainedCertificationEvent).GetTypeInfo().Assembly;
        }

    }
}
