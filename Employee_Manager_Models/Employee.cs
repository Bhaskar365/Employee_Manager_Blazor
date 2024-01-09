using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Employee_Manager_Models
{
    public class Employee
    {

        //[Key]
        //public int UserID { get; set; }
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
        //public string DOB { get; set; } = string.Empty;
        //public string Gender { get; set; } = string.Empty;
        //public string Country { get; set; } = string.Empty;
        //public string City { get; set; } = string.Empty;
        //public string State { get; set; } = string.Empty;
        //public string ZipCode { get; set; } = string.Empty;
        //public string Phone { get; set; } = string.Empty;
        //public string DepartmentId { get; set; } = string.Empty;
        //public string Position { get; set; } = string.Empty;
        //public string DateOfHire { get; set; } = string.Empty;
        //public string CTC { get; set; } = string.Empty;
        //public Department _Department { get; set; }
        //public string CreatedOn { get; set; } = string.Empty;
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmpId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public Address? Address { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
