using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCApplicationTest.BLL.Interfaces;
using MVCApplicationTest.BLL.Repositories;
using MVCApplicationTest.DAL.Contexts;
using MVCApplicationTest.DAL.Models;
using MVCApplicationTestPL.MapperProfiles;


namespace MVCApplicationTestPL
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
            //Add MVC service
            services.AddControllersWithViews();

            //Add DbContext Service
            services.AddDbContext<MVCApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            //Add Repository Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository,EmployeeRepository>();


            //Add Profiler Services
            services.AddAutoMapper(m => m.AddProfile(new DepartmentProfile()));
            services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            services.AddAutoMapper(m => m.AddProfile(new UserProfile()));
            services.AddAutoMapper(m => m.AddProfile(new RoleProfile()));

            //Add Identty Services
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "Account/Login";
                    option.AccessDeniedPath = "Home/Error";
                });
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 4;
                option.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<MVCApplicationDbContext>()
                .AddDefaultTokenProviders();
            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<ApplicationUser>>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
       
    }
}
