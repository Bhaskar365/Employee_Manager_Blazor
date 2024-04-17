using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Manager_API.Interfaces
{
    public interface IExcelUploadRepository
    {
        Task<IActionResult> Upload([FromForm] List<IFormFile> files)
    }
}
