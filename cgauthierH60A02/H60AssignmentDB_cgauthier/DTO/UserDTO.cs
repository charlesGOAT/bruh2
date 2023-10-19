using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace H60AssignmentDB_cgauthier.DTO
{
    [BindProperties]
    public class UserDTO
    {
        public Customer Customer { get; set; }
        public IdentityUser User { get; set; }
        public string Role { get; set; }
      
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //try to use classes on monday
        public UserDTO(IdentityUser user,string role) { 
        Role = role;
        User = user;
        }
        public UserDTO()
        {

        }
        public UserDTO(Customer customer, IdentityUser user,string role)
        {
            Customer = customer;
            User = user;
            Role = role;
        }


    }
}
