using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Employee_Manager_Models
{
    public class Employee
    {

        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string DateOfHire { get; set; } = string.Empty;
        public string CTC { get; set; } = string.Empty;
        public Department _Department { get; set; }
        public string CreatedOn { get; set; } = string.Empty;
    }
}
