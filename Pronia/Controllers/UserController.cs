using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pronia.Helpers;
using Pronia.Helpers.Enums;
using Pronia.Models;
using Pronia.ViewModels;

namespace Pronia.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
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
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        string link = Url.Action("ConfirmEmail", "Auth", new { email = appUser.Email,  token },
         HttpContext.Request.Scheme, HttpContext.Request.Host.Value);
        string body = $"<a href='{link}'>Confirm Email</a>";

        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest
        {
            ToEmail = appUser.Email,
            Subject = "Confirm Email",
            Body = body
        });


        //await _userManager.AddToRoleAsync(appUser, Roles.User.ToString());
        return RedirectToAction("Index", "Home");
    }
    [Authorize]
    public async Task<IActionResult>Profile()
    {
        TempData["Tab"] = "account-dashboard";

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound();

        UserUpdateViewModel userUpdateViewModel = new()
        {
            Fullname = user.Fullname,
            Email = user.Email,
            Username = user.UserName
        };
        UserProfileViewModel userProfileView = new()
        {
            UserUpdateViewModel = userUpdateViewModel,
        };
        return View(userProfileView);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult>UpdateProfile(UserUpdateViewModel userUpdateViewModel)
    {
        TempData["Tab"]= "account-details";

        UserProfileViewModel userProfileViewModel = new()
        {
            UserUpdateViewModel = userUpdateViewModel
        };

        if(!ModelState .IsValid)
            return View(nameof(Profile), userProfileViewModel);

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if(user == null)
            return NotFound();
        if(user.UserName != userUpdateViewModel.Username && _userManager.Users.Any(u=>u.UserName==userUpdateViewModel.Username))
        {
            ModelState.AddModelError("UserName", "Bele bir name artiq movcuddur!");
            return View(nameof(Profile), userProfileViewModel) ;
        }
        if (user.Email != userUpdateViewModel.Email && _userManager.Users.Any(u => u.Email == userUpdateViewModel.Email))
        {
            ModelState.AddModelError("Email", "Bele bir email artiq movcuddur!");
            return View(nameof(Profile), userProfileViewModel);
        }
        if(userUpdateViewModel.CurrentPassword  != null)
        {
            if(userUpdateViewModel.NewPassword == null) 
            {
                ModelState.AddModelError("NewPassword", "Password null ola bilmez");
                return View(nameof(Profile), userProfileViewModel);
            }
          IdentityResult identityResult = await _userManager.ChangePasswordAsync(user, 
              userUpdateViewModel.CurrentPassword, userUpdateViewModel.NewPassword);
        if (!identityResult.Succeeded) 
        {
            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
               return View(nameof(Profile),userProfileViewModel);
            
        }
        }

        user.Fullname = userUpdateViewModel.Fullname;
        user.UserName = userUpdateViewModel.Username;
        user.Email =    userUpdateViewModel.Email;

        IdentityResult result =   await _userManager.UpdateAsync(user);
        if(!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
                return View(nameof(Profile), userProfileViewModel);
        }


        await _signInManager.RefreshSignInAsync(user);

        TempData["SuccessMessage"] = "Sizin profiliniz ugurla yenilendi";

        return RedirectToAction(nameof(Profile));
    }
}

