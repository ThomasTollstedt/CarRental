using CarRental.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarRental
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();

            builder.Services.AddTransient<IBooking, BookingRepository>();
            builder.Services.AddTransient<ICar, CarRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            //using (var scope = app.Services.CreateScope())
            //{ 
            //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


            //    string[] roleNames = { "Customer", "Admin" };
            //    foreach (var roleName in roleNames)
            //    {
            //        if (!await roleManager.RoleExistsAsync(roleName))
            //        {
            //            await roleManager.CreateAsync(new IdentityRole(roleName));
            //        }
            //    }

            //    var adminEmail = "admin@admin.com";
            //    var adminPassword = "Admin123!";
            //    var adminUser = await userManager.FindByEmailAsync(adminEmail);
            //    if (adminUser == null)
            //    {
            //        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
            //        var result = await userManager.CreateAsync(adminUser, adminPassword);
            //        if (result.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(adminUser, "Admin");
            //        }
                    
            //    }


            //}

                app.Run();
        }
    }
}
