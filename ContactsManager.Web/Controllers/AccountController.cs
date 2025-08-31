using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.DTOS;
using ContactsManager.Core.Enums;
using CRUDexample.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
    [Route("Account")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            _roleManager = roleManager;
        }

		[HttpGet]
        [Route("Register")]

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		[Route("Register")]


		public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(registerDTO);


            }
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    PersoName = registerDTO.PersonName,
                    UserName = registerDTO.Email,
                    PhoneNumber = registerDTO.Phone,

                };
              

                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded)
                {
                    if (registerDTO.RoleOptions == RoleOptions.Admin)
                    {
                        if (await _roleManager.FindByNameAsync(registerDTO.RoleOptions.ToString()) is null)
                        {
                            ApplicationRole applicationRole = new ApplicationRole
                            {
                                Name = registerDTO.RoleOptions.ToString(),
                            };
                            await _roleManager.CreateAsync(applicationRole);

                        }
                        await _userManager.AddToRoleAsync(user, RoleOptions.Admin.ToString());


                    }
                    else
                    {

                        await _userManager.AddToRoleAsync(user, RoleOptions.User.ToString());
                    }

                    await   _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Person");

                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("register", error.Description);
                    }

                    return View(registerDTO);

                }
            }
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();

        }

		[HttpPost]
		[Route("Login")]
		public  async Task< IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
		{
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(loginDTO);


            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
					else
						return RedirectToAction("index", "person");


				}
				else
                {

                    ModelState.AddModelError("login", "Email or Password are incorrect");
                    return View(loginDTO);

                }
            }

		}

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "person");

        }

	}
}
