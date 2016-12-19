using System.Linq;
using System.Threading.Tasks;
using Web.Models.AccountViewModels;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly DatabaseContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> loginManager;
        private readonly RoleManager<UserRole> roleManager;
        private readonly ILogger logger;

        /// <summary>
        /// Account controller constructor with dependency injection
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="loginManager"></param>
        /// <param name="roleManager"></param>
        public AccountController(UserManager<User> userManager, DatabaseContext context, SignInManager<User> loginManager, RoleManager<UserRole> roleManager, ILoggerFactory loggerFactory)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            this.context = context;
            this.logger = loggerFactory.CreateLogger<AccountController>();
        }

        /// <summary>
        /// Account default view. Redirect to login page if the user is not logged and
        /// redirect to account page if the user is logged in.
        /// GET: /account/
        /// </summary>
        public async Task<IActionResult> Index()
        {
            User user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return View("Error");
            }

            user = await context.Users.Where(u => u.Id == user.Id).Include(u => u.Setting).SingleAsync();

            IndexViewModel viewModel = new IndexViewModel();

            viewModel.UserName = user.UserName;
            viewModel.PreferedLanguage = user.Setting.PreferedLanguage;
            viewModel.Email = user.Email;
            viewModel.GameDirectory = user.Setting.GameDirectory;
            viewModel.PreferedTheme = user.Setting.PreferedTheme;
            
            return View(viewModel);
        }

        /// <summary>
        /// Account default view. Redirect to login page if the user is not logged and
        /// redirect to account page if the user is logged in.
        /// GET: /account/
        /// </summary>
        /// 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.GetUserAsync(HttpContext.User);

                // If user is trying to change their password 
                if (!string.IsNullOrEmpty(viewModel.Password))
                {
                    // TODO Confirm same password ?

                    var result = await userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.Password);
                    if (result.Succeeded)
                    {
                        await loginManager.SignInAsync(user, isPersistent: false);
                        logger.LogInformation(3, "User " + user.UserName + " changed their password successfully.");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }

                // If there's no password change or the change was successful
                if (ModelState.ErrorCount == 0)
                {
                    Setting settingToUpdate = await context.Settings.SingleOrDefaultAsync(s => s.ID == user.SettingID);

                    settingToUpdate.GameDirectory = viewModel.GameDirectory;
                    settingToUpdate.PreferedLanguage = viewModel.PreferedLanguage;
                    settingToUpdate.PreferedTheme = viewModel.PreferedTheme;

                    user.Email = viewModel.Email;

                    try
                    {
                        context.Update(settingToUpdate);
                        context.Update(user);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SettingExists(settingToUpdate.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        }


        /// <summary>
        /// Account registration view. Redirect to account page if the user is already logged in
        /// GET: /account/register
        /// </summary>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Account registration process. Will validate that the user informations are correct and 
        /// try to register the user and assign the NormalUser role. Redirect to login page on 
        /// successful registration
        /// POST: /account/register
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            // Check the Model Binding Validations
            if (ModelState.IsValid)
            {
                // Build a default setting entity
                Setting setting = new Setting()
                {
                    GameDirectory = "C:\\Program Files (x86)\\Steam",
                    PreferedTheme = Setting.Theme.Default,
                    PreferedLanguage = Setting.Language.EN              // TODO : Check current browser localization
                };

                // Build the user from view model information
                User user = new User()
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    Setting = setting
                };

                // TODO Confirm password ?
                // Insert the user in Database with password hash
                IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    
                    if (!await roleManager.RoleExistsAsync("NormalUser"))
                    {
                        await roleManager.CreateAsync(new UserRole()
                        {
                            Name = "NormalUser"
                        });
                    }

                    // Add NormalUser role to the user 
                    await userManager.AddToRoleAsync(user, "NormalUser");
                    logger.LogInformation("User " + user.UserName + " created an account");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Error during user creation / validation
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            // Redisplay the view because something failed
            return View(viewModel);
        }

        /// <summary>
        /// Login default view. Redirect to account page if the user is logged in.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            // Expose Return URL to view
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// User login process. 
        /// POST: /account/login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null)
        {
            // Expose Return URL to view
            ViewData["ReturnUrl"] = returnUrl;

            // Check the Model Binding Validations
            if (ModelState.IsValid)
            {
                var result = await loginManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    logger.LogInformation("User " + viewModel.UserName + " logged in");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            // Redisplay the view because something failed
            return View(viewModel);
        }

        /// <summary>
        /// User logoff process. 
        /// POST: /account/logoff
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await loginManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private bool SettingExists(int id)
        {
            return context.Settings.Any(e => e.ID == id);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
}
}
