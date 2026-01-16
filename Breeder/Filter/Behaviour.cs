namespace Breeder.Filter;

/**
 * Cette enum est triée dans l'ordre de priorité des
 * opérations sql.
 * L'ordre est le suivant: SELECT, FROM, WHERE, GROUP BY, HAVING, ORDER BY et LIMIT
 * source: https://learnsql.com/blog/sql-order-of-operations/#:~:text=Six%20Operations%20to%20Order%3A%20SELECT,BY%2C%20HAVING%2C%20and%20ORDER%20BY
 * Cet ordre est respecté grâce à l'ordre de l'enum
 */
public enum FilterBehaviour
{
    Group_By,
    Having,
    Order_By_Desc,
    Order_By_Asc,
    Limit
}

public class FilterBehaviourAdapterImpl : IFilterFieldAdapter<FilterBehaviour>
{
    public string? GetField(FilterBehaviour filter)
    {
        return filter switch
        {
            FilterBehaviour.Order_By_Desc => "order by {0} desc",
            FilterBehaviour.Order_By_Asc => "order by {0} asc", // Asc est la valeur par défaut d'un order by
            FilterBehaviour.Group_By => "group by {0}",
            FilterBehaviour.Limit => "limit {0}",
            _ => null
        };
    }
}

public class FilterBehaviourValue : IFilterBehaviourValue
{
    readonly FilterBehaviour _filter;
    readonly string _statement;

    FilterBehaviourValue(FilterBehaviour filter, string statement)
    {
        _filter = filter;
        _statement = statement;
    }

    public FilterBehaviour GetFilter() => _filter;
    public string GetStatement() => _statement;

    public static IFilterBehaviourValue Of(FilterBehaviour filter, string statement)
    {
        return new FilterBehaviourValue(filter, statement);
    }
}

public interface IFilterBehaviourValue
{
    FilterBehaviour GetFilter();
    string GetStatement();
}

