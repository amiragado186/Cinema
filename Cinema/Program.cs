using Cinema.Repositories;
using Cinema.DataAcess;
using Cinema.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Cinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
         

            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped < IPriceRepository , PriceRepository > ();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=customer}/{controller=movie}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
