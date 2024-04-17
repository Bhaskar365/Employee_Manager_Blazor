using Microsoft.AspNetCore.Components.Forms;

namespace Employee_Manager_API.Interfaces
{
    public interface IExcelUploadRepository
    {
        Task UploadExcelData(IBrowserFile file);
    }
}
