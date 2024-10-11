using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia.Models;
using Pronia.ViewModels;
using System.Net;

namespace Pronia.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var signInManager = await _signInManager.PasswordSignInAsync(user,loginViewModel.Password,loginViewModel.RememberMe,false);
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
}
