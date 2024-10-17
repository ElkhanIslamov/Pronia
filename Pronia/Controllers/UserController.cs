using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using NuGet.Common;
using Pronia.Helpers;
=======
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
using Pronia.Helpers.Enums;
using Pronia.Models;
using Pronia.ViewModels;

namespace Pronia.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
<<<<<<< HEAD
    private readonly IConfiguration _configuration;

    public UserController(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
=======

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    }

    public IActionResult Register()
    {
<<<<<<< HEAD
        if (User.Identity.IsAuthenticated)
        return RedirectToAction("Home", "Index");

=======
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
<<<<<<< HEAD
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Home", "Index");

        if (!ModelState.IsValid)
           return View();
        
        AppUser appUser = new AppUser()
        {
            Fullname = registerViewModel.Fullname,
            Email = registerViewModel.Email,
            UserName = registerViewModel.Username,
            IsActive = true
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser,registerViewModel.Password);
        if(!identityResult.Succeeded)
        {
           foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
                    
            };
            return View();
        }
        string token =await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        string link = Url.Action("ConfirmEmail", "Auth", new { email = appUser.Email, token = token }, HttpContext.Request.Scheme, HttpContext.Request.Host.Value);
        string body = $"<a href='{link}'>Confirm your password</a>";
        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest
        {
            ToEmail = appUser.Email,
            Subject = "Confirm Email",
            Body = body
        });


        //await _userManager.AddToRoleAsync(appUser,Roles.User.ToString());
=======
        if(!ModelState.IsValid) 
            return View();

        AppUser appUser = new AppUser()
        {
            Fullname = registerViewModel.Fullname,
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email
            
        };
        IdentityResult identityResult = await _userManager.CreateAsync(appUser,registerViewModel.Password);
        if (!identityResult.Succeeded) 
        {
            foreach(var error in identityResult.Errors) 
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }
<<<<<<< HEAD
        await _userManager.AddToRoleAsync(appUser, Roles.User.ToString()); 
=======
       await _userManager.AddToRoleAsync(appUser,Roles.User.ToString());
>>>>>>> 7da07ac27062cb0bdbab6832752d563215280c78
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
        return RedirectToAction("Index", "Home");
    }
}
