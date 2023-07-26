using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.AdminRepo;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.CityRepo;
using Shipping_MVC.Repository.CustomerRepo;
using Shipping_MVC.Repository.DeliveryRepo;
using Shipping_MVC.Repository.EmployeeRepo;
using Shipping_MVC.Repository.GovernroateRepo;
using Shipping_MVC.Repository.OrderAdminRepo;
using Shipping_MVC.Repository.OrderCustomerRepo;
using Shipping_MVC.Repository.OrderDeliveryRepo;
using Shipping_MVC.Repository.OrderEmployeeRepo;
using Shipping_MVC.Repository.WeightRepo;

namespace Shipping_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ShippingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("shipping"));
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IGovernroateRepository, GovernroateRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IOrderAdminRepositorycs, OrderAdminRepository>();
            builder.Services.AddScoped<IOrderEmployeeRepository, OrderEmployeeRepository>();
            builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
            builder.Services.AddScoped<IOrderDeliveryRepository, OrderDeliveryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IWeightRepository, WeightRepository>();
            builder.Services.AddScoped<IAdminRepositrory, AdminRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}