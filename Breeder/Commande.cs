using MySql.Data.MySqlClient;

namespace Breeder;

public class Commande
{
    public int Id { get; set; }
    public string Libelle { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public int IdFournisseur { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as Commande);

    public bool Equals(Commande? statut)
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
        return Id == statut.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Commande? lhs, Commande? rhs)
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

    public static bool operator !=(Commande? lhs, Commande? rhs) => !(lhs == rhs);
}

public interface ICommandeDao
{
    List<Commande> GetCommandes();
    List<Commande> GetCommandesRecentes(int nombre);
    int GetLastAddedIdCommande();
    Commande? GetCommande(int id);
    void AjouterCommande(string libelle, DateTime date, decimal total, int idFournisseur);
    void SupprimerCommande(int id);
    void ModifierCommande(Commande commande);
}

public interface ICommandeFacade
{
    List<Commande> GetCommandes();
    List<Commande> GetCommandesRecentes();
    int GetLastAddedIdCommande();
    Commande? GetCommande(int id);
    void AjouterCommande(string libelle, DateTime date, decimal total, int idFournisseur);
    void SupprimerCommande(int id);
    void ModifierCommande(Commande commande);
}

public class CommandeDaoImpl : ICommandeDao
{
    private readonly MySqlConnection _connection;

    public CommandeDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<Commande> GetCommandes()
    {
        MySqlCommand command = new MySqlCommand("select id, libelle, dateCommande, total, idFournisseur from commande;",
            _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Commande> commandes = new List<Commande>();
        while (reader.Read())
        {
            Commande commande = new Commande();
            commande.Id = reader.GetInt32("id");
            commande.Libelle = reader.GetString("libelle");
            commande.Date = reader.GetDateTime("dateCommande");
            commande.Total = reader.GetDecimal("total");
            commande.IdFournisseur = reader.GetInt32("idFournisseur");
            commandes.Add(commande);
        }

        reader.Close();
        return commandes;
    }

    public List<Commande> GetCommandesRecentes(int nombre)
    {
        MySqlCommand command = new MySqlCommand("select id, libelle, dateCommande, total, idFournisseur from commande order by dateCommande desc limit @nombre;",
            _connection);
        command.Parameters.AddWithValue("nombre", nombre);
        MySqlDataReader reader = command.ExecuteReader();

        List<Commande> commandes = new List<Commande>();
        while (reader.Read())
        {
            Commande commande = new Commande();
            commande.Id = reader.GetInt32("id");
            commande.Libelle = reader.GetString("libelle");
            commande.Date = reader.GetDateTime("dateCommande");
            commande.Total = reader.GetDecimal("total");
            commande.IdFournisseur = reader.GetInt32("idFournisseur");
            commandes.Add(commande);
        }

        reader.Close();
        return commandes;
    }

    public int GetLastAddedIdCommande()
    {
        MySqlCommand command = new MySqlCommand("select max(id) from commande where id != @id;", _connection);
        command.Parameters.AddWithValue("id", 1);
        return (int)command.ExecuteScalar();
    }

    public Commande? GetCommande(int id)
    {
        MySqlCommand command =
            new MySqlCommand("select libelle, dateCommande, total, idFournisseur from commande where id = @id;",
                _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Commande commande = new Commande();
        while (reader.Read())
        {
            commande.Id = id;
            commande.Libelle = reader.GetString("libelle");
            commande.Date = reader.GetDateTime("dateCommande");
            commande.Total = reader.GetDecimal("total");
            commande.IdFournisseur = reader.GetInt32("idFournisseur");
        }

        reader.Close();
        return commande;
    }

    public void SupprimerCommande(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from commande where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public void ModifierCommande(Commande commande)
    {
        MySqlCommand command =
            new MySqlCommand(
                "update commande set libelle = @libelle, dateCommande = @date, total = @total, idFournisseur = @idFournisseur where id = @id;",
                _connection);
        command.Parameters.AddWithValue("id", commande.Id);
        command.Parameters.AddWithValue("libelle", commande.Libelle);
        command.Parameters.AddWithValue("date", commande.Date);
        command.Parameters.AddWithValue("total", commande.Total);
        command.Parameters.AddWithValue("idFournisseur", commande.IdFournisseur);
        command.ExecuteNonQuery();
    }

    public void AjouterCommande(string libelle, DateTime date, decimal total, int idFournisseur)
    {
        MySqlCommand command =
            new MySqlCommand(
                "insert into commande(libelle, dateCommande, total, idFournisseur) values(@libelle, @date, @total, @idFournisseur);",
                _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.Parameters.AddWithValue("date", date);
        command.Parameters.AddWithValue("total", total);
        command.Parameters.AddWithValue("idFournisseur", idFournisseur);
        command.ExecuteNonQuery();
    }
}

public class CommandeFacadeImpl : ICommandeFacade
{
    private readonly ICommandeDao _commandeDao;

    public CommandeFacadeImpl(ICommandeDao commandeDao)
    {
        _commandeDao = commandeDao;
    }

    public List<Commande> GetCommandes()
    {
        return _commandeDao.GetCommandes();
    }
    
    public List<Commande> GetCommandesRecentes()
    {
        return _commandeDao.GetCommandesRecentes(5);
    }

    public int GetLastAddedIdCommande()
    {
        return _commandeDao.GetLastAddedIdCommande();
    }

    public Commande? GetCommande(int id)
    {
        return _commandeDao.GetCommande(id);
    }

    public void SupprimerCommande(int id)
    {
        _commandeDao.SupprimerCommande(id);
    }

    public void ModifierCommande(Commande commande)
    {
        _commandeDao.ModifierCommande(commande);
    }

    public void AjouterCommande(string libelle, DateTime date, decimal total, int idFournisseur)
    {
        _commandeDao.AjouterCommande(libelle, date, total, idFournisseur);
    }
}