// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Edu_Store.Enums;
using Edu_Store.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Edu_Store.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> userManager;

        public LoginModel( SignInManager<ApplicationUser> signInManager , ILogger<LoginModel> logger , UserManager<ApplicationUser> userManager )
        {
            _signInManager = signInManager;
            _logger = logger;
            this.userManager = userManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType( DataType.Password )]
            public string Password { get; set; }
            [Display( Name = "Remember me?" )]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync( string returnUrl = null )
        {

            if ( !string.IsNullOrEmpty( ErrorMessage ) )
            {
                ModelState.AddModelError( string.Empty , ErrorMessage );
            }

            returnUrl ??= Url.Content( "~/" );

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync( IdentityConstants.ExternalScheme );

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync( )).ToList( );

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync( string returnUrl = null )
        {


            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync( )).ToList( );

            if ( ModelState.IsValid )
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync( Input.Email , Input.Password , Input.RememberMe , lockoutOnFailure: false );
                if ( result.Succeeded )
                {
                    _logger.LogInformation( "User logged in." );
                    var userRoles = await userManager.GetRolesAsync( await userManager.FindByEmailAsync( Input.Email ) );
                    if ( userRoles.Contains( nameof( Roles.Teacher ) ) )
                    {
                        _logger.LogInformation( "User in teacher role" );
                        return RedirectToAction( "index" , "TeacherDashboard" );
                    }
                    if ( userRoles.Contains( nameof( Roles.Student ) ) )
                    {
                        _logger.LogInformation( "User in student role" );
                        return RedirectToAction( "index" , "Home" );
                    }
                }
                if ( result.RequiresTwoFactor )
                {
                    return RedirectToPage( "./LoginWith2fa" , new { ReturnUrl = returnUrl , RememberMe = Input.RememberMe } );
                }
                if ( result.IsLockedOut )
                {
                    _logger.LogWarning( "User account locked out." );
                    return RedirectToPage( "./Lockout" );
                }
                else
                {
                    ModelState.AddModelError( string.Empty , "Invalid login attempt." );
                    return Page( );
                }
            }

            // If we got this far, something failed, redisplay form
            return Page( );
        }
    }
}
