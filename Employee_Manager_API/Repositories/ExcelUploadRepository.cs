using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Employee_Manager_API.Repositories
{
    public class ExcelUploadRepository : IExcelUploadRepository
    {
        private readonly AppDbContext _context;

        public ExcelUploadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task UploadExcelData(IBrowserFile file)
        {
            try
            {
                if (file == null || file.Size == 0)
                {
                    throw new ArgumentException("File is not selected or empty.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(memoryStream);
                    var fileData = memoryStream.ToArray();

                    // Save the file data into the database
                    var exportExcel = new ExportExcel
                    {
                        FileName = file.Name,
                        FileData = fileData,
                        UploadDateTime = DateTime.UtcNow
                    };

                    _context.TblExportExcel.Add(exportExcel);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file: {ex.Message}");
            }
        }
    }
}