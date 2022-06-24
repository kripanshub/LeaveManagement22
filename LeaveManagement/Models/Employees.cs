using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    public class Employees
    {
        public int Id { get; set; }
      
        public string EmpId { get; set; }

        [Required(ErrorMessage = "Please enter UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string Confirmpwd { get; set; }

        [Required(ErrorMessage = "Please enter EmpName")]
        [Display(Name = "EmpName")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter MobileNo")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "MobileNo")]        
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Please enter DOB")]
        [Display(Name = "DOB")]
        public  string DOB { get; set; }
        [Required(ErrorMessage = "Please enter Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter Qualification")]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Please enter Designation")]
        [Display(Name = "Designation")]
        public string  Designation { get; set; }

        [Required(ErrorMessage = "Please enter EmpType")]
        [Display(Name = "EmpType")]
        public string EmpType { get; set; }

        [Required(ErrorMessage = "Please enter DOJ")]
        [Display(Name = "DOJ")]
        public string DOJ { get; set; }

    }
}
