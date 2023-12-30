using AnwiamEyeClinicServices.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnwiamEyeClinicServices
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("AnwiamServicesDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //var connectionString2 = builder.Configuration.GetConnectionString("DefaultConnection2") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
    //        builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString2));
          
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.MapRazorPages();

            using(var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "admin", "doctor", "opd", "pharmacy","account","revenue" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            //creates a user and assignes role

            //using (var scope = app.Services.CreateScope())
            //{
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //    string name = "john";
            //    string password = "1234";
            //    if(await userManager.FindByNameAsync(name) == null)
            //    {
            //        var user = new IdentityUser()
            //        {
            //            UserName = name
            //        };
            //        await userManager.CreateAsync(user,password);
            //        await userManager.AddToRoleAsync(user, "admin");
            //    }
            //}

                app.Run();
        }
    }
}