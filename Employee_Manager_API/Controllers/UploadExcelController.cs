using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Logic.Services;
using Employee_Manager_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadExcelController : ControllerBase
    {

        private readonly IExcelUploadRepository _uploadService;
        public UploadExcelController(IExcelUploadRepository uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost("uploadExcel")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is not selected or empty.");
                }

                // Call the UploadExcelData method of the AdminService
                await _uploadService.UploadExcelData(file);

                return Ok(new { FilePath = "File uploaded successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }
    }
}