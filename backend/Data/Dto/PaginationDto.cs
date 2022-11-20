namespace backend.Data.Dto;

public class PaginationDto
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int PageItemsFrom { get; private set; }
    public int PageItemsTo { get; private set; }
    public bool HasPrevious { get; private set; }
    public bool HasNext { get; private set; }
    public int PreviousPage { get; private set; }
    public int NextPage { get; private set; }

    public PaginationDto(int currentPage, int totalPages, int pageSize, int totalCount, int pageItemsFrom, int pageItemsTo, bool hasPrevious, bool hasNext, int previousPage, int nextPage)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
        PageItemsFrom = pageItemsFrom;
        PageItemsTo = pageItemsTo;
        HasPrevious = hasPrevious;
        HasNext = hasNext;
        PreviousPage = previousPage;
        NextPage = nextPage;
    }
}
