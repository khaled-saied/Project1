using Demo.BLL.Profiles;
using Demo.BLL.Services.AttachmentServices;
using Demo.BLL.Services.DepartmentsServices;
using Demo.BLL.Services.ServicesOfEmployee;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Models.IdentityModel;
using Demo.DAL.Repositories.Classes;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            #region Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            //builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            //Department
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            //Employee
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();

            //AutoMapper
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(M=> M.AddProfile(new MappingProfiles()));
            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Attachment Services
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();

            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                             .AddEntityFrameworkStores<ApplicationDbContext>();

            #endregion

            var app = builder.Build();


            #region Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Account}/{action=Register}/{id?}");

                pattern: "{controller=Account}/{action=Register}/{id?}");

            #endregion

            app.Run();
        }
    }
}
