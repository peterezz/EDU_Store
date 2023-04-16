using Edu_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Edu_Store
{
    public class Program
    {
        public static void Main( string[ ] args )
        {
            var builder = WebApplication.CreateBuilder( args );

            // Add services to the container.
            #region MVC Service
            builder.Services.AddControllersWithViews( );
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "591786662170-7etbht75kl1rn8in3ahhicg4mi9o3djd.apps.googleusercontent.com";
                options.ClientSecret = "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";
            })/*.AddFacebook(options =>
            {
                options.ClientId = "1197300410930086";
                options.ClientSecret = "8a307a63f2b21878bba21eec1568b14a";
            })*/;
            #endregion

            #region Default DbContext service
            var connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" ) ?? throw new InvalidOperationException( "Connection string 'DefaultConnection' not found." );
            builder.Services.AddDbContext<ApplicationDbContext>( options =>
                options.UseSqlServer( connectionString ) );
            builder.Services.AddDatabaseDeveloperPageExceptionFilter( );
            #endregion

            #region Identity service
            builder.Services.AddDefaultIdentity<IdentityUser>( options => options.SignIn.RequireConfirmedAccount = true )
        .AddEntityFrameworkStores<ApplicationDbContext>( );
            #endregion

            var app = builder.Build( );

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment( ) )
            {
                app.UseMigrationsEndPoint( );
            }
            else
            {
                app.UseExceptionHandler( "/Home/Error" );
            }
            app.UseStaticFiles( );

            app.UseRouting( );

            app.UseAuthorization( );

            app.MapControllerRoute(
                name: "default" ,
                pattern: "{controller=Home}/{action=Index}/{id?}" );
            app.MapRazorPages( );

            app.Run( );
        }
    }
}