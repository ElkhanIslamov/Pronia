using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia.Helpers.Enums;
using Pronia.Models;
using Pronia.ViewModels;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Pronia.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            RedirectToAction("Index", "Home");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if(User.Identity.IsAuthenticated)
          RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View();

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
        if(signInManager.IsLockedOut)
        {
            ModelState.AddModelError("", "User is blocked...");
        }

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
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
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
    {
        if (!ModelState.IsValid)
            return View();
        var user = await _userManager.FindByEmailAsync(email);
  

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

    //public async Task<IActionResult> CreateRole()
    //{
    //    foreach (var roleName in Enum.GetNames(typeof(Roles)))
    //    {
    //        await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
    //    }
    //    //await _roleManager.CreateAsync(new IdentityRole { Name="Admin" });
    //    //await _roleManager.CreateAsync(new IdentityRole { Name="Moderator" });
    //    //await _roleManager.CreateAsync(new IdentityRole { Name="User" });

    //    return Content("Roles created");
    //}

}
