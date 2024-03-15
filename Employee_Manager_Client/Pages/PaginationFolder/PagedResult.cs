namespace Employee_Manager_Client.Pages.PaginationFolder
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public T[]? Results { get; set; }
    }
}