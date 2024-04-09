using DocumentFormat.OpenXml.Drawing;
using Employee_Manager_API.Helper;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;
using Employee_Manager_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportOpenXMLController : ControllerBase
    {
        //private readonly IConfiguration configuration;
        //private readonly string DbConnection;
        private readonly IEmployeeRepository _employeeRepository;
        //public ExportOpenXMLController(IConfiguration _configuration, IEmployeeRepository employeeRepository)
        //{
        //    configuration = _configuration;
        //    DbConnection = configuration["connectionStrings:DbConnection"] ?? "";
        //    _employeeRepository = employeeRepository;
        //}
        public ExportOpenXMLController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(Employee[] employee) 
        {
            try 
            {
                List<Employee> employeeDetails = new List<Employee>();

                foreach (var emp in employeeDetails) 
                {
                    var empForUpload = new Employee
                    {
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Email = emp.Email,
                        Gender = emp.Gender,
                        DOB = emp.DOB,
                        AddressId = emp.AddressId,
                        Address = emp.Address,
                        DepartmentId = emp.DepartmentId,
                        Department = emp.Department,
                        JoiningDate = emp.JoiningDate,
                    };
                    employeeDetails.Add(emp);
                }
                return Ok("Successful");
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> ImportEmployee_In_docx()
        //{
        //    var files = Request.Form.Files;
        //    if (!files.Any())
        //    {
        //        return BadRequest();
        //    }

        //    // Ensure only one file is uploaded
        //    if (files.Count != 1)
        //    {
        //        return BadRequest("Please upload only one file.");
        //    }

        //    // Check file extension
        //    var file = files.First();
        //    var fileExtension = System.IO.Path.GetExtension(file.FileName);
        //    if (fileExtension != ".docx" && fileExtension != ".xlsx" && fileExtension != ".doc")
        //    {
        //        return BadRequest("Only .doc and .xlsx files are supported.");
        //    }

        //    // Continue processing the file based on its type
        //    if (fileExtension == ".docx")
        //    {
        //        var fileName = await Helper.FileHelper.Upload(file);
        //        var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
        //        var word = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(path, true);
        //        var paragraphs = word.MainDocumentPart.Document.Body.Descendants<Paragraph>().ToList();

        //        // Your existing code for reading and processing the document goes here

        //        var employeeDetails = new ExportModel()
        //        {
                    
        //            // Populate employee properties based on document content
        //        };

        //        return Ok(employeeDetails);
        //    }

        //    else
        //    {
        //        // This part should not be reached, but handle any unexpected cases
        //        return BadRequest("Unsupported file format other than document.");
        //    }
        //}


        //[HttpGet]
        //public IActionResult DownloadExcelFile()
        //{
        //    DataSet dataSet = new DataSet();
        //    //Configure DB connection string in appsetting.json
        //    using (SqlConnection connection = new SqlConnection(DbConnection))
        //    {
        //        /* for multi Sheet either enter multiple 'Select' statement as inline query
        //        or get multiple result set from Stored Procedure from DB*/
        //        SqlDataAdapter adapter = new SqlDataAdapter
        //        {
        //            SelectCommand = new SqlCommand("Select * from Tbl_Employee", connection)
        //        };
        //        adapter.Fill(dataSet);
        //    }
        //    return File(ExportHelper.ExportToExcelDownload(dataSet), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "YourExcel.xls");
        //}
    }
}
