
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Employee_Manager_Client.Services
{
    public class FrontendUserPanelService : IFrontendUserPanelService
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedSessionStorage _sessionStorage;
        public FrontendUserPanelService(ProtectedSessionStorage sessionStorage, HttpClient httpclient) 
        {
            _sessionStorage = sessionStorage;
            _httpClient = httpclient;
        }
        public async Task<bool> IsUserLoggedIn()
        {
            bool flag = false;
            var result = await _sessionStorage.GetAsync<string>("adminKey");
            if (result.Success)
            {
                flag = true;
            }
            return flag;
        }


    }
}
