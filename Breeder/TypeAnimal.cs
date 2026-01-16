using System.Collections;
using MySql.Data.MySqlClient;

namespace Breeder;

public class TypeAnimal
{
    public int Id { get; set; }

    public string Libelle { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as TypeAnimal);

    public bool Equals(TypeAnimal? statut)
    {
        if (statut is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (ReferenceEquals(this, statut))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (GetType() != statut.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (Id == statut.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(TypeAnimal? lhs, TypeAnimal? rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(TypeAnimal lhs, TypeAnimal rhs) => !(lhs == rhs);
}

public interface ITypeDao : InconnuRequired
{
    List<TypeAnimal> GetTypes();
    List<TypeAnimal> GetTypesSansInconnu();
    TypeAnimal? GetType(int id);
    void AjouterType(string libelle);
    void SupprimerType(int idType);
}

public interface ITypeFacade : InconnuRequired, InconnuProvider<TypeAnimal>
{
    List<TypeAnimal> GetTypes();
    List<TypeAnimal> GetTypesSansInconnu();
    TypeAnimal? GetType(int id);
    void AjouterType(string libelle);
    void SupprimerType(int id);
}


public class TypeDaoImpl : ITypeDao
{
    private readonly MySqlConnection _connection;

    public TypeDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<TypeAnimal> GetTypes()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleType from typeanimal;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<TypeAnimal> typeAnimals = new List<TypeAnimal>();
        while (reader.Read())
        {
            TypeAnimal typeAnimal = new TypeAnimal();
            typeAnimal.Id = reader.GetInt32("id");
            typeAnimal.Libelle = reader.GetString("libelleType");
            typeAnimals.Add(typeAnimal);
        }

        reader.Close();
        return typeAnimals;
    }

    public List<TypeAnimal> GetTypesSansInconnu()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleType from typeanimal where id != @id;", _connection);
        command.Parameters.AddWithValue("id", 1);
        MySqlDataReader reader = command.ExecuteReader();

        List<TypeAnimal> typeAnimals = new List<TypeAnimal>();
        while (reader.Read())
        {
            TypeAnimal typeAnimal = new TypeAnimal();
            typeAnimal.Id = reader.GetInt32("id");
            typeAnimal.Libelle = reader.GetString("libelleType");
            typeAnimals.Add(typeAnimal);
        }

        reader.Close();
        return typeAnimals;
    }

    public TypeAnimal? GetType(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelleType from typeanimal where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        TypeAnimal typeAnimal = new TypeAnimal();
        typeAnimal.Id = id;
        
        while (reader.Read())
        {
            typeAnimal.Libelle = reader.GetString("libelleType");
        }

        reader.Close();
        return typeAnimal;
    }

    public void SupprimerType(int idType)
    {
        MySqlCommand command = new MySqlCommand("delete from typeanimal where id = @id;", _connection);
        command.Parameters.AddWithValue("id", idType);
        command.ExecuteNonQuery();
    }

    public void AjouterType(string libelle)
    {
        MySqlCommand command = new MySqlCommand("insert into typeanimal(libelleType) values(@libelle);", _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.ExecuteNonQuery();
    }

    public void AjouterInconnu()
    {
        MySqlCommand command = new MySqlCommand("insert into typeanimal(id, libelleType) values(@id, @libelle);", _connection);
        command.Parameters.AddWithValue("id", 1);
        command.Parameters.AddWithValue("libelle", "Inconnu");
        command.ExecuteNonQuery();
    }
}

public class TypeFacadeImpl : ITypeFacade
{
    private readonly ITypeDao _typeDao;

    public TypeFacadeImpl(ITypeDao typeDao)
    {
        _typeDao = typeDao;
    }
    
    public void AjouterInconnu()
    {
        _typeDao.AjouterInconnu();
    }

    public TypeAnimal GetInconnu()
    {
        TypeAnimal? inconnu = GetType(1);
        if (inconnu != null)
        {
            return inconnu;
        }
        AjouterInconnu();
        inconnu = GetType(1) ?? throw new Exception(string.Format(MessageUtils.Erreur_Inconnu, "Types"));
        return inconnu;
    }

    public List<TypeAnimal> GetTypes()
    {
        return _typeDao.GetTypes();
    }

    public List<TypeAnimal> GetTypesSansInconnu()
    {
        return _typeDao.GetTypesSansInconnu();
    }

    public TypeAnimal? GetType(int id)
    {
        return _typeDao.GetType(id);
    }

    public void AjouterType(string libelle)
    {
        _typeDao.AjouterType(libelle);
    }

    public void SupprimerType(int id)
    {
        _typeDao.SupprimerType(id);
    }
}