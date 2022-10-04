using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Lab1.Models
{
    /// <summary>
    /// Application user Modal class for Identityuser's
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user
        /// </summary>
        [Required, Display(Name = "First Name"), StringLength(128)]
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a user
        /// </summary>
        [Required, Display(Name = "Last Name"), StringLength(128)]
        public String LastName { get; set; }

        /// <summary>
        /// Date time variable to store Date of Birth
        /// </summary>
        [Display(Name = "Birth Date")]
        public DateTime ?DateOfBirth { get; set; } 


    }
}
