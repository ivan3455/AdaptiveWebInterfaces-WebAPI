using System.ComponentModel.DataAnnotations;

namespace AdaptiveWebInterfaces_WebAPI.Models.User
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The lastname is required")] // атрибут забезпечення валідації даних
        [StringLength(15, ErrorMessage = "The maximum number of characters is 15")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [StringLength(15, ErrorMessage = "The maximum number of characters is 15")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Minimum password length is 8")]
        public string Password { get; set; }
        
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsBlocked { get; set; }
    }
}
