using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Manager_Models.Models
{
    public class ExportExcel
    {
        [Key]
        public int Id { get; set; }

        public string? ExcelCode { get; set; }
        public byte[]? ExcelFile { get; set; }
    }
}
