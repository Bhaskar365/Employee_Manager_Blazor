namespace Employee_Manager_API.Interfaces
{
    public interface IExcelUploadRepository
    {
        Task UploadExcelData(IFormFile file);
    }
}
