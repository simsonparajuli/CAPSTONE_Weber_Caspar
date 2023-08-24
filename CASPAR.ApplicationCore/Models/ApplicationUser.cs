using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace CASPAR.ApplicationCore.Models;

[Index(nameof(WNumber), IsUnique = true)]
public class ApplicationUser : IdentityUser
{
    /*	
        public ApplicationUser()
        {
            // It looks like there does not need to be anything else ATM
            // We will need to update this when the we receive the design
            // documents after June 21
        }
    */

    [Required]
    [DisplayName("W#")]
    public string WNumber { get; set; }

    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    // Email, Password is on AspNetUsers
}
