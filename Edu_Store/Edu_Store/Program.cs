using Edu_Store.Managers;
using Edu_Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Extensions.DependencyInjection;

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
                options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;

            });
            //.AddFacebook(options =>
            //{
            //    options.AppId = "611643087536377";
            //    options.AppSecret = "7fdb754495764c6c8f7e84b180d42a22";
            //})
            // builder.Services.AddScoped<TeacherManager>( );

            builder.Services.AddScoped<CourseManager>( );
         builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));
            builder.Services.AddSendGrid(Option => {
                Option.ApiKey = builder.Configuration.GetSection("SendGridSettings").GetValue<string>("ApiKey");
                
                
                });
            //logout 
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "ExampleSession";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.IsEssential = true;
            });

            #endregion
            #region SendGrid SMTP(Email Confirmaiton)
            builder.Services.AddScoped<CourseManager>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));
            builder.Services.AddSendGrid(Option => {
                Option.ApiKey = builder.Configuration.GetSection("SendGridSettings").GetValue<string>("ApiKey");


            });
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
            //GetCurrent User id service
            builder.Services.AddHttpContextAccessor();

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