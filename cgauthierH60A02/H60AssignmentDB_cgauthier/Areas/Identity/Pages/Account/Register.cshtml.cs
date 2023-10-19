// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ModelsLibrary;
using Newtonsoft.Json.Linq;

namespace H60AssignmentDB_cgauthier.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;

        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IStoreRepository<Customer> _storeRepository;
        

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IStoreRepository<Customer> storeRepository)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _storeRepository = storeRepository;
            
            
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public enum Provinces
        {
            AB,

            BC,

            MB,

            NB,

            NL,

            NT,

            NS,

            NU,

            ON,

            PE,

            QC,

            SK,

            YT
        }

       

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "Username")]
            
            public string Username { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string Role { get; set; }
            [Required(ErrorMessage = "The First name field is required")]
            [StringLength(16)]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "The Last name field is required")]
            [StringLength(16, MinimumLength = 0)]
            public string LastName { get; set; }
            [Required]
            [RegularExpression("[0-9]{10}",ErrorMessage = "Format should be 9999999999")]
            public string PhoneNumber { get; set; }
            [Required]
            public string Province { get; set; }
            [RegularExpression("([0-9]{4}\\ ){3}[0-9]{4}",ErrorMessage ="Format should be 9999 9999 9999 9999")]
            
            public string CreditCard { get; set; }

        }

        public async Task<bool> GetUsernameExists() {
            var usernameExists = _storeRepository.GetAllAsync().Result.Any(x => x.Username == Input.Username);
            if (usernameExists) {
                TempData["error"] = "Someone with the same username already exists";
                    }
            return usernameExists;
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (User.IsInRole("Customer")||!_signInManager.IsSignedIn(User)) {
                ViewData["data"] = new SelectList(_roleManager.Roles.Where(x => x.NormalizedName == "Customer"), "NormalizedName", "NormalizedName");
            }
            else if (User.IsInRole("Clerk"))
            {
                ViewData["data"] = new SelectList(_roleManager.Roles.Where(x => x.NormalizedName == "Customer" || x.NormalizedName == "Clerk"), "NormalizedName", "NormalizedName");
            }
            else
            {
                ViewData["data"] = new SelectList(_roleManager.Roles, "NormalizedName", "NormalizedName");
            }
                ViewData["province"] = new SelectList(Enum.GetValues(typeof(Provinces)));
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (User.IsInRole("Customer") || !_signInManager.IsSignedIn(User))
            {
                ViewData["data"] = new SelectList(_roleManager.Roles.Where(x => x.NormalizedName == "Customer"), "NormalizedName", "NormalizedName");
            }
            else if (User.IsInRole("Clerk"))
            {
                ViewData["data"] = new SelectList(_roleManager.Roles.Where(x => x.NormalizedName == "Customer" || x.NormalizedName == "Clerk"), "NormalizedName", "NormalizedName");
            }
            else
            {
                ViewData["data"] = new SelectList(_roleManager.Roles, "NormalizedName", "NormalizedName");
            }
          
            var rolesData = (SelectList)ViewData["data"];
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (Input.Role != "Customer")
            {
                ModelState["Input.FirstName"].ValidationState = ModelValidationState.Valid;
                ModelState["Input.LastName"].ValidationState = ModelValidationState.Valid;
                ModelState["Input.PhoneNumber"].ValidationState = ModelValidationState.Valid;
                ModelState["Input.Province"].ValidationState = ModelValidationState.Valid;
                ModelState["Input.CreditCard"].ValidationState = ModelValidationState.Valid;
            }
            
            if (ModelState.IsValid && !(await GetUsernameExists()))
            {
                var user = CreateUser();
                var stateOk = true;
                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                try
                {
                    await _emailStore.FindByEmailAsync(Input.Email, CancellationToken.None);
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "This email is already registered");
                    stateOk = false;
                }
                if (!rolesData.Any(x=>x.Value == Input.Role)) {
                    TempData["RoleErr"] = "Please choose a valid role";
                    stateOk = false;
                }

                if (!Enum.IsDefined(typeof(Provinces), Input.Province) && (User.IsInRole("Customer") || !_signInManager.IsSignedIn(User))) {
                    TempData["ProvinceErr"] = "Please choose a valid province";
                    stateOk = false;
                }

                if (!stateOk) {
                    return Page();
                }
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //TODO Add customer
                    if (Input.Role == "Customer") {
                        var customer = new Customer() { Username = Input.Username, FirstName = Input.FirstName, LastName = Input.LastName, CreditCard = Input.CreditCard, Email = Input.Email, PhoneNumber = Input.PhoneNumber, Province = Input.Province };
                        await _storeRepository.Create(customer);
                    }
                    
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, Input.Role);
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        TempData["loggedin"] = true;
                        // await _signInManager.SignInAsync(user, isPersistent: false);
                        //bruh
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
