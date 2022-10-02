using invoice_project.Models;
using invoice1_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace invoice1_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("Cs");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Entity>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IinvoiceRepository,invoiceRepository>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=invoice}/{action=new}/{id?}");

            app.Run();
        }
    }
}