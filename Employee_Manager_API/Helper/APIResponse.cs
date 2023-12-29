namespace Employee_Manager_API.Helper
{
    public class APIResponse
    {
        public int ResponseCode { get; set; }
        public string Result { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
