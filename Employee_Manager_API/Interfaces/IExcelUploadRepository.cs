namespace Employee_Manager_API.Interfaces
{
    public interface IExcelUploadRepository
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
    }
}
