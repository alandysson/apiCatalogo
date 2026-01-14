namespace ApiCatalogo.DTOs.Base;

public class RestResponse<T> where T : class
{
    public RestResponse()
    {

    }

    public RestResponse(T data, PaginatorResponse? paginator = null)
    {
        Data = data;
        Paginator = paginator;
    }

    public T Data { get; set; } = null!;

    public PaginatorResponse? Paginator { get; set; }
}