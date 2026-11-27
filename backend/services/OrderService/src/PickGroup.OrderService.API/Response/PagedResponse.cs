using PickGroup.OrderService.Application.Common.Pagination;

namespace PickGroup.OrderService.API.Response;

public record PagedResponse<T>
{
    public IEnumerable<T> Data { get; }
    
    public int PageNumber { get; }
    
    public int PageSize { get; }
    
    public int TotalRecords { get; }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

    public PagedResponse(
        PagedResult<T> results)
    {
        Data = results.Results;
        PageNumber = results.PageNumber;
        PageSize = results.PageSize;
        TotalRecords = results.TotalCount;
    }
    
    public static PagedResponse<T> ToPagedResponse(
        PagedResult<T> results) => new PagedResponse<T>(results);
}