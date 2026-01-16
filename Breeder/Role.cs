using MySql.Data.MySqlClient;

namespace Breeder;

public class Role
{
    public int Id { get; set; }

    public string Libelle { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as Role);

    public bool Equals(Role? statut)
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

    public static bool operator ==(Role? lhs, Role? rhs)
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

    public static bool operator !=(Role? lhs, Role? rhs) => !(lhs == rhs);
}

public interface IRoleDao : InconnuRequired
{
    List<Role> GetRoles();
    Role? GetRole(int id);
}

public interface IRoleFacade : InconnuRequired, InconnuProvider<Role>
{
    List<Role> GetRoles();
    Role? GetRole(int id);
}

public class RoleDaoImpl : IRoleDao
{
    private readonly MySqlConnection _connection;

    public RoleDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public void AjouterInconnu()
    {
        throw new NotImplementedException();
    }

    public List<Role> GetRoles()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleRole from role;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Role> roles = new List<Role>();
        while (reader.Read())
        {
            Role role = new Role();
            role.Id = reader.GetInt32("id");
            role.Libelle = reader.GetString("libelleRole");
            roles.Add(role);
        }

        reader.Close();
        return roles;
    }

    public Role? GetRole(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelleRole from role where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Role role = new Role();
        while (reader.Read())
        {
            role.Id = id;
            role.Libelle = reader.GetString("libelleRole");
        }

        reader.Close();
        return role;
    }
}

public class RoleFacadeImpl : IRoleFacade
{
    private readonly IRoleDao _roleDao;

    public RoleFacadeImpl(IRoleDao roleDao)
    {
        _roleDao = roleDao;
    }
    
    public void AjouterInconnu()
    {
        _roleDao.AjouterInconnu();
    }

    public Role GetInconnu()
    {
        Role? inconnu = GetRole(1);
        if (inconnu != null)
        {
            return inconnu;
        }
        _roleDao.AjouterInconnu();
        inconnu = GetRole(1) ?? throw new Exception(String.Format(MessageUtils.Erreur_Inconnu, "Role"));
        return inconnu;
    }

    public List<Role> GetRoles()
    {
        return _roleDao.GetRoles();
    }

    public Role? GetRole(int id)
    {
        return _roleDao.GetRole(id);
    }
}