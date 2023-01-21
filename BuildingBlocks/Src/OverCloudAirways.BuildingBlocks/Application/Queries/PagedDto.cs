namespace DArch.Application.Contracts;

public class PagedDto<T>
{
    public PagedDto() { }

    public T[] Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
