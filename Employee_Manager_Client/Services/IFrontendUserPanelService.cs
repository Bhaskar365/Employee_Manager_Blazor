namespace Employee_Manager_Client.Services
{
    public interface IFrontendUserPanelService
    {
        Task<bool> IsUserLoggedIn();
    }
}
