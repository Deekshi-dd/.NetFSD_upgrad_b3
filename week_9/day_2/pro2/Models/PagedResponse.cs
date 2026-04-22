namespace ContactPagingAPI.Models
{
    public class PagedResponse<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }
}
