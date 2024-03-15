namespace Employee_Manager_Client.Pages.PaginationFolder
{
    public class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; } = 5;
        public int RowCount { get; set; } = 5;
        public int FirstRowOnPage => Math.Min((CurrentPage - 1) * PageSize + 1, RowCount);
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
    }
}
