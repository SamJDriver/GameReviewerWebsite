namespace Components
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = default!;
        public int TotalRowCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}