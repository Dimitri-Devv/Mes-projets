using MySql.Data.MySqlClient;

namespace Breeder.Filter;

public enum FilterOperation
{
    EQUAL,
    DIFFERENT,
    IN
}

public interface IFilterFieldAdapter<in T>
{
    string? GetField(T filter);
}

public interface IFilterFieldValue<out T>
{
    T GetFilter();
    FilterOperation GetOperation();
    string GetValue();
}

public interface IFilterReadStrategy<out T>
{
    T Read(MySqlDataReader reader);
}

public interface IFilterProvider<T, in U>
{
    List<T> GetWithFilters(string statement, IFilterFieldValue<U>[] filterFieldValue, IFilterBehaviourValue[] filterBehaviourValues);
}

public class FilterOperationAdapterImpl : IFilterFieldAdapter<FilterOperation>
{
    public string? GetField(FilterOperation filter)
    {
        return filter switch
        {
            FilterOperation.IN => "in",
            FilterOperation.EQUAL => "=",
            FilterOperation.DIFFERENT => "!=",
            _ => null
        };
    }
}

public class PrimitiveFilterReadStrategy : IFilterReadStrategy<object>
{
    private Type _primitive;
    private string _field;

    public PrimitiveFilterReadStrategy(Type primitive, string field)
    {
        _primitive = primitive;
        _field = field;
    }
    
    public object Read(MySqlDataReader reader)
    {
        if (_primitive == typeof(string))
        {
            return reader.GetString(_field);
        }

        if (_primitive == typeof(Int32))
        {
            return reader.GetInt32(_field);
        }

        return null;
    }
}

public class AbstractFilterFieldValue<T> : IFilterFieldValue<T>
{
    private T _filter;
    private FilterOperation _operation;
    private string _value;

    private AbstractFilterFieldValue(T filter, FilterOperation operation, string value)
    {
        _filter = filter;
        _operation = operation;
        _value = value;
    }

    public T GetFilter()
    {
        return _filter;
    }

    public FilterOperation GetOperation()
    {
        return _operation;
    }

    public string GetValue()
    {
        return _value;
    }

    public static AbstractFilterFieldValue<T> Of(T filter, FilterOperation operation, string value)
    {
        return new AbstractFilterFieldValue<T>(filter, operation, value);
    }
}

public class FilterProviderImpl<T, U> : IFilterProvider<T, U>
{
    private readonly MySqlConnection _connection;
    private readonly IFilterReadStrategy<T> _filterReadStrategy;
    private readonly IFilterFieldAdapter<U> _filterFieldAdapter;
    private readonly IFilterFieldAdapter<FilterOperation> _filterOperationAdapter;
    private readonly IFilterFieldAdapter<FilterBehaviour> _filterBehaviourAdapter;

    public FilterProviderImpl(MySqlConnection connection, IFilterReadStrategy<T> filterReadStrategy, IFilterFieldAdapter<U> filterFieldAdapter, IFilterFieldAdapter<FilterOperation> filterOperationAdapter, IFilterFieldAdapter<FilterBehaviour> filterBehaviourAdapter)
    {
        _connection = connection;
        _filterReadStrategy = filterReadStrategy;
        _filterFieldAdapter = filterFieldAdapter;
        _filterOperationAdapter = filterOperationAdapter;
        _filterBehaviourAdapter = filterBehaviourAdapter;
    }

    public List<T> GetWithFilters(string statement, IFilterFieldValue<U>[] filterFieldValue, IFilterBehaviourValue[] filterBehaviourValues)
    { 
        MySqlCommand command = new MySqlCommand(statement, _connection);

        for (int i = 0; i < filterFieldValue.Length; i++)
        {
            IFilterFieldValue<U> filterValue = filterFieldValue[i];
            string? field = _filterFieldAdapter.GetField(filterValue.GetFilter());
            string value = filterValue.GetValue();

            if (field == null)
            {
                continue;
            }

            command.CommandText += field + " " + _filterOperationAdapter.GetField(filterValue.GetOperation()) + " @" + field + (i > 0 ? "and " : " ");
            command.Parameters.AddWithValue(field, value);
        }

        // Trier les opérations du FilterBehaviour pour respecter l'ordre sql
        List<IFilterBehaviourValue> behaviourValues = filterBehaviourValues.ToList();
        behaviourValues.Sort((x, y) => x.GetFilter().CompareTo(y.GetFilter()));
        
        /**
         * TODO Pour utiliser des opérations sur plusieurs champs à la fois
         * exemple: order by nom, prenom
         */
        for (int i = 0; i < behaviourValues.Count; i++)
        {
            IFilterBehaviourValue behaviourValue = behaviourValues[i];
            FilterBehaviour filterBehaviour = behaviourValue.GetFilter();
            string? behaviourSql = _filterBehaviourAdapter.GetField(filterBehaviour);
            string sqlStatement = behaviourValue.GetStatement();

            if (behaviourSql == null)
                continue;

            command.CommandText += string.Format(behaviourSql, sqlStatement) + (i + 1 == behaviourValues.Count ? "" : " ");
        }

        command.CommandText += ";";
        Console.WriteLine("Sql Statement: " + command.CommandText);

        MySqlDataReader reader = command.ExecuteReader();
        
        List<T> list = new List<T>();
        while (reader.Read())
        {
            list.Add(_filterReadStrategy.Read(reader));
        }

        reader.Close();
        return list;
    }
}