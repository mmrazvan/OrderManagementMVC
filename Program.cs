using OrderManagementMVC.Models;
using OrderManagementMVC.Repositories;

namespace OrderManagementMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<OrderManagementContext, OrderManagementContext>();
            builder.Services.AddTransient<LabelsRepository, LabelsRepository>();
            builder.Services.AddTransient<OrdersRepository, OrdersRepository>();
            builder.Services.AddTransient<OrderLabelsRepository, OrderLabelsRepository>();
            builder.Services.AddTransient<OrderTraceRepository, OrderTraceRepository>();

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