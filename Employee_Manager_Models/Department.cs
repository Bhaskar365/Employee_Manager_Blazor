using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Manager_Models
{
    public class Department
    {
        //public int Id { get; set; }
        //public string DepartmentId { get; set; }
        //public string DepartmentName { get; set; } = string.Empty;
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
