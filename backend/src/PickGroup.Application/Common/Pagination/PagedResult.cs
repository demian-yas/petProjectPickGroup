namespace PickGroup.Application.Common.Pagination;

public record PagedResult<T>(
    IEnumerable<T> Results,
    int PageNumber,
    int PageSize,
    int TotalCount);