using Microsoft.EntityFrameworkCore;

namespace backend.Helpers;

public class PaginatedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public int PageItemsFrom => (CurrentPage - 1) * PageSize + 1;
    public int PageItemsTo => PageItemsFrom + PageSize > TotalCount ? TotalCount : PageSize + (CurrentPage - 1) * PageSize;
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
    public int PreviousPage => CurrentPage - 1;
    public int NextPage => CurrentPage + 1;

    public PaginatedList(List<T> items, int count, int page, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = page;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, page, pageSize);
    }
}
