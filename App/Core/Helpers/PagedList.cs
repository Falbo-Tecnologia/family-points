namespace Core.Helpers;

public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> items, int total, int currentPage, int pageSize)
    {
        this.AddRange(items);
        Total = total;
        CurrentPage = currentPage;
        PageSize = pageSize;
        Pages = (int)Math.Ceiling(Total / (double)PageSize);
    }

    public int Total { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int Pages { get; set; }
}
