namespace ApiCatalogo.DTOs.Base;

public class PaginatorResponse
{
    public PaginatorResponse(int currentPage, int totalRecords, int pageSize, int totalOnThisPage)
    {
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        CurrentPage = currentPage;
        TotalRecords = totalRecords;
        TotalPages = totalPages >= 1 ? totalPages : 1;
        TotalOnThisPage = totalOnThisPage;
    }

    public int TotalPages { get; }

    public int TotalRecords { get; }

    public int CurrentPage { get; }

    public int TotalOnThisPage { get; set; }
}