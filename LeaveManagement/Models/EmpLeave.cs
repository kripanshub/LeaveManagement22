using System.ComponentModel.DataAnnotations;
namespace LeaveManagement.Models
{
    public class EmpLeave
    {
        
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter Leave Type")]
        [Display(Name = "LeaveType")]
        public string LeaveType { get; set; }

        [Required(ErrorMessage = "Please enter StartDate")]
        [Display(Name = "StartDate")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Please enter EndDate")]
        [Display(Name = "EndDate")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "Please enter Reasons")]
        [Display(Name = "Reasons")]
        public string Reasons { get; set; }

        public string HRApproval { get; set; }

    }
}
