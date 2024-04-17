using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Logic.Services;
using Employee_Manager_Models.Models;
using Microsoft.AspNetCore.Components.Forms;
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

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest("No files selected or empty.");
                }

                foreach (var file in files)
                {
                    // Convert IFormFile to IBrowserFile
                    var browserFile = new InputFile(file);

                    // Call the UploadExcelData method of the service
                    await _uploadService.UploadExcelData(browserFile);
                }

                return Ok(new { Message = "Files uploaded successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading files: {ex.Message}");
            }
        }
    }
}