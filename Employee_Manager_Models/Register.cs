using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Manager_Models
{
    public class Register
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty; 
        public DateTime CreatedOn { get; set; }

    }
}
