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