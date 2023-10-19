using CustomerSideProject.Data;
using CustomerSideProject.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;

namespace CustomerSideProject.Controllers
{
    public class AccountsController : Controller
    {
        public readonly IStoreRepository<Customer> _storeRepository;

        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly H60AssignmentDB_cgauthierContext _context;
        
        public AccountsController(IStoreRepository<Customer> storeRepository, IUserStore<IdentityUser> userStore, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, H60AssignmentDB_cgauthierContext context )
        {
            _context = context;
            _signInManager = signInManager;

            _storeRepository = storeRepository;

            _userManager = userManager;
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                var currentUser = await _signInManager.UserManager.GetUserAsync(User);



                var user = _context.Users.First(x => x.Id == id);
                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Clerk" && (await _userManager.GetRolesAsync(user)).First() == "Manager")
                {
                    return Forbid();
                }

                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Customer" && currentUser.Id != user.Id)
                {
                    return Forbid();
                }

                var role = (await _userManager.GetRolesAsync(user)).First();
                if (role == "Customer")
                {
                    var myUser = (await _storeRepository.GetAllAsync()).First(x => x.Username == user.UserName);

                    return View(new UserDTO(myUser, user, role));
                }


                return View(new UserDTO(user, role));
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserDTO userDTO) {
            try
            {
                //check for when username is the same
                var currentUser = await _signInManager.UserManager.GetUserAsync(User);
                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Clerk" && (await _userManager.GetRolesAsync(userDTO.User)).First() == "Manager")
                {
                    return Forbid();
                }

                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Customer" && currentUser.Id != userDTO.User.Id)
                {
                    return Forbid();
                }


                ModelState["Password"].ValidationState = ModelValidationState.Valid;
                ModelState["ConfirmPassword"].ValidationState = ModelValidationState.Valid;
                if (ModelState["User.Email"].ValidationState == ModelValidationState.Valid && ModelState["User.UserName"].ValidationState == ModelValidationState.Valid && userDTO.Role == "Customer")
                {
                    userDTO.Customer.Email = userDTO.User.Email;
                    userDTO.Customer.Username = userDTO.User.UserName;
                    ModelState["Customer.Email"].ValidationState = ModelValidationState.Valid;
                    ModelState["Customer.Username"].ValidationState = ModelValidationState.Valid;

                }

                if (_context.Users.Select(x=>x.UserName).Any(x=>x == userDTO.User.UserName) && currentUser.UserName != userDTO.User.UserName ) {
                    TempData["error"] = "There cannot be two people with the same username";
                    return View(userDTO);
                }

                if (ModelState.IsValid)
                {

                   await _storeRepository.Update(userDTO.Customer);
                    var theUser = await _signInManager.UserManager.FindByIdAsync(userDTO.User.Id);
                    theUser.Email = userDTO.User.Email;
                    theUser.UserName = userDTO.User.UserName;
                    await _userManager.UpdateAsync(theUser);
                    _context.SaveChanges();
                   


                   
                    return RedirectToAction("Index", controllerName: "Home");
                }
                return View(userDTO);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }



        [HttpGet]

        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                var user = _context.Users.First(x => x.Id == id);


                var currentUser = await _signInManager.UserManager.GetUserAsync(User);



                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Clerk" && (await _userManager.GetRolesAsync(user)).First() == "Manager")
                {
                    return Forbid();
                }

                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Customer" && currentUser.Id != user.Id)
                {
                    return Forbid();
                }


                var role = (await _userManager.GetRolesAsync(user)).First();
                if (role == "Customer")
                {
                    var myUser = (await _storeRepository.GetAllAsync()).First(x => x.Username == user.UserName);

                    return View(new UserDTO(myUser, user, role));
                }


                return View(new UserDTO(user, role));
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
        //cannot delete your own account

        [HttpPost]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var currentUser = await _signInManager.UserManager.GetUserAsync(User);
                var user = _context.Users.First(x => x.Id == id);


                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Clerk" && (await _userManager.GetRolesAsync(user)).First() == "Manager")
                {
                    return Forbid();
                }

                if ((await _userManager.GetRolesAsync(currentUser)).First() == "Customer" && currentUser.Id != user.Id)
                {
                    return Forbid();
                }

                try
                {
                    await _storeRepository.Delete((await _storeRepository.GetAllAsync()).First(x => x.Username == user.UserName).CustomerId);
                }
                catch
                {
                }
                List<IdentityUser> list = new List<IdentityUser>();
                foreach (var identityUser in _context.Users)
                {
                    if (await _userManager.IsInRoleAsync(identityUser, "Manager"))
                    {
                        list.Add(identityUser);
                    }
                }



                if (list.Count <= 1 && await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    TempData["Error"] = "You cannot delete the last manager";
                    return RedirectToAction("Index");
                }

                _context.Remove(_context.Users.First(x => x.Id == id));
                _context.SaveChanges();

                if (currentUser == user)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", controllerName: "Home");
                }


     

                return RedirectToAction("Index",controllerName:"Home");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public IActionResult Get(int id) {
            return View();
        }

        public IActionResult Index() { 
        return View();
        }

        public IActionResult Error() {
            return View();
        }

    }
}
