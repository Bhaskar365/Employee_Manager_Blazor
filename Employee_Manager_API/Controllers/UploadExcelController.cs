using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadExcelController : ControllerBase
    {
        private readonly string _uploadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is not selected or empty.");
                }

                // Ensure the upload directory exists
                if (!Directory.Exists(_uploadDirectory))
                {
                    Directory.CreateDirectory(_uploadDirectory);
                }

                // Generate a unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Construct the file path
                string filePath = Path.Combine(_uploadDirectory, fileName);

                // Save the file to the specified location
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }
    }
}