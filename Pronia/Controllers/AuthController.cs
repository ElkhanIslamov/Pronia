<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia.Helpers;
using Pronia.Helpers.Enums;
using Pronia.Models;
using Pronia.ViewModels;
=======
﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using NuGet.Protocol;
>>>>>>> 7da07ac27062cb0bdbab6832752d563215280c78
using Pronia.Helpers.Enums;
using Pronia.Models;
using Pronia.ViewModels;
using System.Net;
using System.Security.Cryptography.X509Certificates;
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740

namespace Pronia.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
<<<<<<< HEAD
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
=======

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
<<<<<<< HEAD
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
=======
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
<<<<<<< HEAD
            return RedirectToAction("Index", "Home");

        return View();
    }

=======
            RedirectToAction("Index", "Home");
        return View();
    }
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
<<<<<<< HEAD
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
=======
        if(User.Identity.IsAuthenticated)
          RedirectToAction("Index", "Home");
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740

        if (!ModelState.IsValid)
            return View();

<<<<<<< HEAD
        var user = await _userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or Password is incorrect");
                return View();
            }
        }
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            ModelState.AddModelError("", "Please confirm your email");
            return View();
        }
        var signInManager = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
        if (!signInManager.Succeeded)
        {
            ModelState.AddModelError("", "Username/Email or Password is incorrect");
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> LogOut()
    {
        if (!User.Identity.IsAuthenticated)
            return BadRequest();

        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
=======
        var user = await _userManager.FindByNameAsync(loginViewModel.UserNameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(loginViewModel.UserNameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "username/email or password is incorrect");
                return View();

            }
        }
        var signInManager = await _signInManager.PasswordSignInAsync(user,loginViewModel.Password,loginViewModel.RememberMe,true);
<<<<<<< HEAD
        if(signInManager.IsLockedOut)
        {
            ModelState.AddModelError("", "User is blocked...");
        }

=======
        if(!signInManager.IsLockedOut) 
        {
            ModelState.AddModelError("", "Blok edildi...");
            return View();  
        }
>>>>>>> 7da07ac27062cb0bdbab6832752d563215280c78
        if (!signInManager.Succeeded)
        {
            ModelState.AddModelError("", "username/email or password is incorrect");
            return View();

        }


        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult>Logout()
    {
        if(!User.Identity.IsAuthenticated)
            return BadRequest();

        _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");   
    }
<<<<<<< HEAD
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
<<<<<<< HEAD
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
=======
    public async Task<IActionResult>ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    {
        if (!ModelState.IsValid)
            return View();
        var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
        if (user == null)
        {
            ModelState.AddModelError("Email", "Email not found");
            return View();
        }
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
<<<<<<< HEAD
        string link = Url.Action("ResetPassword", "Auth", new { email = user.Email, token = token }, HttpContext.Request.Scheme, HttpContext.Request.Host.Value);

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "templates", "index.html");
        using StreamReader streamReader = new StreamReader(path);
        string content = await streamReader.ReadToEndAsync();
        string body = content.Replace("[link]", link);

        // string body = $"<a href='{link}'>Reset your password</a>";

        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest
        {
            ToEmail = user.Email,
            Subject = "ResetPassword",
            Body = body
        });
        return RedirectToAction(nameof(Login));
    }
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
        if (user == null)
            return NotFound();

        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(SubmitPasswordViewModel submitPasswordViewModel, string email, string token)
=======
        string link = Url.Action("ResetPassword","Auth", new {email= user.Email,token},
            HttpContext.Request.Scheme, HttpContext.Request.Host.Value);
        return Content(link);
        
           

        return RedirectToAction(nameof(Login));
    }
    public async Task<IActionResult>ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
    {
      
        var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
        if (user == null)
          return NotFound();

        return View();       
    }
    [HttpPost]
    [ValidateAntiForgeryToken]  
    public async Task<IActionResult>ResetPassword(SubmitPasswordViewModel submitPasswordViewModel,string email,string token)
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    {
        if (!ModelState.IsValid)
            return View();
        var user = await _userManager.FindByEmailAsync(email);
<<<<<<< HEAD
        if (user == null)
            return NotFound();
        IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, token, submitPasswordViewModel.Password);
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
                return View();

            }
        }

        return RedirectToAction(nameof(Login));

    }
    public async Task<IActionResult> ConfirmEmail(ConfirmViewModel confirmViewModel)
    {
        var user = await _userManager.FindByEmailAsync(confirmViewModel.Email);
        if (user == null)
            return NotFound();

        if (await _userManager.IsEmailConfirmedAsync(user))
            return BadRequest();

        IdentityResult identityResult = await _userManager.ConfirmEmailAsync(user, confirmViewModel.Token);
        if (identityResult.Succeeded)
        {
            TempData["ConfirmationMessage"] = "Your email succesfully confirmed";
            return RedirectToAction(nameof(Login));
        }
        return BadRequest();// To do error page;
    }
=======
  

        IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, token,submitPasswordViewModel.Password);
        if (!identityResult.Succeeded) 
        {
            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("",error.Description);
                return View();
                
            }
        }
        return RedirectToAction(nameof(Login));
    }

>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    //public async Task<IActionResult> CreateRole()
    //{
    //    foreach (var roleName in Enum.GetNames(typeof(Roles)))
    //    {
    //        await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
    //    }
<<<<<<< HEAD
    //    //await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
    //    //await _roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
    //    //await _roleManager.CreateAsync(new IdentityRole { Name = "User" });



    //    return Content("Role created");
    //}

=======
    //    //await _roleManager.CreateAsync(new IdentityRole { Name="Admin" });
    //    //await _roleManager.CreateAsync(new IdentityRole { Name="Moderator" });
    //    //await _roleManager.CreateAsync(new IdentityRole { Name="User" });

    //    return Content("Roles created");
    //}

=======
    public async Task<IActionResult> CreateRole()
    {
        foreach (var userRole in Enum.GetNames(typeof(Roles)))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = userRole });
        }
        return View();
    }
>>>>>>> 7da07ac27062cb0bdbab6832752d563215280c78
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
}
