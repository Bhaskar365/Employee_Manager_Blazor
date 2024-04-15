using Employee_Manager_API.Interfaces;

namespace Employee_Manager_API.Repositories
{
    public class ExcelUploadRepository : IExcelUploadRepository
    {
        private readonly string _uploadDirectory;

        public ExcelUploadRepository(string uploadDirectory)
        {
            _uploadDirectory = uploadDirectory;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                // Ensure the upload directory exists
                if (!Directory.Exists(_uploadDirectory))
                {
                    Directory.CreateDirectory(_uploadDirectory);
                }

                // Generate a unique file name
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                // Construct the file path
                string filePath = Path.Combine(_uploadDirectory, uniqueFileName);

                // Save the file to the specified location
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(stream);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                throw new Exception($"Error saving file: {ex.Message}");
            }
        }
    }
}