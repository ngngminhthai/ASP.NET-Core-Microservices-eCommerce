namespace Shared.SeedWork;

public class PagingRequestParameters
{
    public PagingRequestParameters()
    {
        
    }
    private const int maxPageSize = 50;

    private int _pageIndex = 1;

    private int _pageSize = 10;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > 0)
                _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}