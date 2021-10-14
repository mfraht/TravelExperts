// -------------------------------------------------------------------------------- //
// -------------------------- REGISTER VIEW MODEL --------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using System.ComponentModel.DataAnnotations;


namespace TravelExperts.Models
{
    // -------------------- Definition of Register View Model class --------------------------------- //
    // ----------------------------------- start ---------------------------------------------------- //
    public class RegisterViewModel
    {
        // -------------------- Data Validation for Username ------------------------------- //

        [Required(ErrorMessage = "Please enter a Username.")]
        [StringLength(255, ErrorMessage = "The Username cannot exceed 255 characters. ")]
        public string Username { get; set; }

        // -------------------- Data Validation for First Name ------------------------------- //

        [Required(ErrorMessage = "Please enter a First Name.")]
        [StringLength(255, ErrorMessage = "The First Name cannot exceed 255 characters. ")]
        public string Firstname { get; set; }

        // -------------------- Data Validation for Last Name ------------------------------- //

        [Required(ErrorMessage = "Please enter a Last Name.")]
        [StringLength(255, ErrorMessage = "The Last Name cannot exceed 255 characters. ")]
        public string Lastname { get; set; }

        // ------------------ Data Validation for Email Address ----------------------------- //

        [Required(ErrorMessage = "Please enter an Email Address.")]
        [StringLength(255, ErrorMessage = "The Email cannot exceed 255 characters. ")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+.[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]*$", ErrorMessage = "Please enter a valid email address. ")]
        public string Email { get; set; }

        // -------------------- Data Validation for Password ------------------------------- //

        [Required(ErrorMessage = "Please enter a Password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        // -------------------- Data Validation for Confirm Password ------------------------------- //

        [Required(ErrorMessage = "Please confirm your Password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }

    // -------------------- Definition of Register View Model class --------------------------------- //
    // ----------------------------------- end ---------------------------------------------------- //
}
