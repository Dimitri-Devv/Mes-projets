using MySql.Data.MySqlClient;

namespace Breeder;

public class User
{
    public int Id { get; set; }
    public string Pseudo { get; set; }
    public string Mail { get; set; }
    public string MotDePasse { get; set; }

    public override string ToString()
    {
        return Pseudo;
    }
    
    public override bool Equals(object obj) => Equals(obj as User);

    public bool Equals(User? statut)
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

    public static bool operator ==(User? lhs, User? rhs)
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

    public static bool operator !=(User? lhs, User? rhs) => !(lhs == rhs);
}

public interface IUserDao
{
    List<User> GetUtilisateurs();
    User? GetUtilisateur(int id);
    bool VerifierConnexion(string pseudo, string password);
}

public interface IUserFacade
{
    List<User> GetUtilisateurs(); 
    User? GetUtilisateur(int id);
    bool VerifierConnexion(string pseudo, string password);
}


public class UserDaoImpl : IUserDao
{
    private readonly MySqlConnection _connection;

    public UserDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<User> GetUtilisateurs()
    {
        MySqlCommand command = new MySqlCommand("select id, pseudo, mail, mdp from utilisateur;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<User> users = new List<User>();
        while (reader.Read())
        {
            User user = new User();
            user.Id = reader.GetInt32("id");
            user.Pseudo = reader.GetString("pseudo");
            user.Mail = reader.GetString("mail");
            user.MotDePasse = reader.GetString("mdp");

            users.Add(user);
        }

        reader.Close();
        return users;
    }

    public User? GetUtilisateur(int id)
    {
        MySqlCommand command = new MySqlCommand("select pseudo, mail, mdp from utilisateur where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        User user = new User();
        while (reader.Read())
        {
            user.Id = id;
            user.Pseudo = reader.GetString("pseudo");
            user.Mail = reader.GetString("mail");
            user.MotDePasse = reader.GetString("mdp");
        }

        reader.Close();
        return user;
    }

    public bool VerifierConnexion(string pseudo, string password)
    {
        MySqlCommand command = new MySqlCommand("select exists(select id from utilisateur where pseudo = @pseudo and mdp = sha2(@password, 256));", _connection);
        command.Parameters.AddWithValue("pseudo", pseudo);
        command.Parameters.AddWithValue("password", password);
        int exists = Convert.ToInt32(command.ExecuteScalar());
        return exists > 0;
    }
}

public class UserFacadeImpl : IUserFacade
{
    private readonly IUserDao _userDao;

    public UserFacadeImpl(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public List<User> GetUtilisateurs()
    {
        return _userDao.GetUtilisateurs();
    }

    public User? GetUtilisateur(int id)
    {
        return _userDao.GetUtilisateur(id);
    }

    public bool VerifierConnexion(string pseudo, string password)
    {
        return _userDao.VerifierConnexion(pseudo, password);
    }
}