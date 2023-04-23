using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.AddRazorPages( );
            builder.Services.AddAuthentication( ).AddGoogle( options =>
            {
                options.ClientId = "591786662170-7etbht75kl1rn8in3ahhicg4mi9o3djd.apps.googleusercontent.com";
                options.ClientSecret = "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";
            } ).AddTwitter( options =>
            {
                options.ConsumerKey = "SVB3VXFVWGcyaUg5NkdwSDZ2MFQ6MTpjaQ";
                options.ConsumerSecret = "xzrwsMPywVJBFNqB3VavwuGWHnERcapW0Ww0Q_xkWIntElCwaH";
            } );
            //.AddFacebook(options =>
            //{
            //    options.AppId = "611643087536377";
            //    options.AppSecret = "7fdb754495764c6c8f7e84b180d42a22";
            //})
            // builder.Services.AddScoped<TeacherManager>( );
            builder.Services.AddScoped<CourseManager>( );

            #endregion

            #region Default DbContext service
            var connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" ) ?? throw new InvalidOperationException( "Connection string 'DefaultConnection' not found." );
            builder.Services.AddDbContext<ApplicationDbContext>( options =>
                options.UseSqlServer( connectionString ) );

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).
                AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter( );



            #endregion

            #region Identity service
            builder.Services.AddIdentityCore<ApplicationUser>( options => options.SignIn.RequireConfirmedAccount = true )
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
            app.UseEndpoints(endpoint=>endpoint.MapRazorPages( ) ); 

            app.Run( );

        }
    }
}