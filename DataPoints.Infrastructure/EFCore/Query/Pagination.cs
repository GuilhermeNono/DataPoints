using System.Linq.Expressions;
using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Domain.Enums;

namespace DataPoints.Infrastructure.EFCore.Query;

public sealed class Pagination<TResult>: IPagination<TResult>
{
    public Pagination(int? page, int? size)
    {
        Ordering = new List<IOrder<TResult>>();
        Page = page;
        Size = size;
    }

    public int? Page { get; set; }
    public int? Size { get; set; }

    public bool IsPageable => Size > 0;
    public bool IsLastInOrder(IOrder<TResult> orderItem) => Ordering.Last()!.Equals(orderItem);
    public bool IsSortable => Ordering.Count > 0;

    public List<IOrder<TResult>> Ordering { get; }

    public void OrderBy<TProperty>(Expression<Func<TResult, TProperty>> expression, Sort sort)
    {
        var newOrdering = new Order<TResult>();

        newOrdering.By(expression, sort);
        
        Ordering.Add(newOrdering);
    }
    
    public void OrderBy(string customOrder, Sort sort)
    {
        var newOrdering = new Order<TResult>();

        newOrdering.By(customOrder, sort);
        
        Ordering.Add(newOrdering);
    }

    public void Validate()
    {
        if (Page is 0)
            Page = 1;
        
        if ((Page is null && Size is not null) || Page < 0)
            Page = 1;
    }
}
