namespace PickGroup.API.Response;

public record PagedResponse<T>
{
    public IEnumerable<T> Data { get; }
    
    public int PageNumber { get; }
    
    public int PageSize { get; }
    
    public int TotalRecords => Data.Count();
    
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

    public PagedResponse(
        IEnumerable<T> data,
        int pageNumber,
        int pageSize)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    
    public static PagedResponse<T> ToPagedResponse(
        IEnumerable<T> data,
        int pageNumber,
        int pageSize) => new PagedResponse<T>(data, pageNumber, pageSize);
}