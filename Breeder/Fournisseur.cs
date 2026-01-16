using MySql.Data.MySqlClient;

namespace Breeder;

public class Fournisseur
{
    public int Id { get; set; }
    public string Libelle { get; set; }
    public string Adresse { get; set; }
    public string Mail { get; set; }
    public string Telephone { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as Fournisseur);

    public bool Equals(Fournisseur? statut)
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

    public static bool operator ==(Fournisseur? lhs, Fournisseur? rhs)
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

    public static bool operator !=(Fournisseur? lhs, Fournisseur? rhs) => !(lhs == rhs);
    
}

public interface IFournisseurDao : InconnuRequired
{
    List<Fournisseur> GetFournisseurs();
    List<Fournisseur> GetFournisseursSansInconnu();
    Fournisseur? GetFournisseur(int id);
    void AjouterFournisseur(string libelle, string adresse, string mail, string numero);
    void SupprimerFournisseur(int id);
    void ModifierFournisseur(Fournisseur fournisseur);
}

public interface IFournisseurFacade : InconnuRequired, InconnuProvider<Fournisseur>
{
    List<Fournisseur> GetFournisseurs();
    List<Fournisseur> GetFournisseursSansInconnu();
    Fournisseur? GetFournisseur(int id);
    void AjouterFournisseur(string libelle, string adresse, string mail, string numero);
    void SupprimerFournisseur(int id);
    void ModifierFournisseur(Fournisseur fournisseur);
}


public class FournisseurDaoImpl : IFournisseurDao
{
    private readonly MySqlConnection _connection;

    public FournisseurDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<Fournisseur> GetFournisseurs()
    {
        MySqlCommand command =
            new MySqlCommand("select id, libelle, adresse, mail, numero from fournisseur;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Fournisseur> fournisseurs = new List<Fournisseur>();
        while (reader.Read())
        {
            Fournisseur fournisseur = new Fournisseur();
            fournisseur.Id = reader.GetInt32("id");
            fournisseur.Libelle = reader.GetString("libelle");
            fournisseur.Adresse = reader.GetString("adresse");
            fournisseur.Mail = reader.GetString("mail");
            fournisseur.Telephone = reader.GetString("numero");
            fournisseurs.Add(fournisseur);
        }

        reader.Close();
        return fournisseurs;
    }

    public List<Fournisseur> GetFournisseursSansInconnu()
    {
        MySqlCommand command =
            new MySqlCommand("select id, libelle, adresse, mail, numero from fournisseur where id != @id;", _connection);
        command.Parameters.AddWithValue("id", 1);
        MySqlDataReader reader = command.ExecuteReader();

        List<Fournisseur> fournisseurs = new List<Fournisseur>();
        while (reader.Read())
        {
            Fournisseur fournisseur = new Fournisseur();
            fournisseur.Id = reader.GetInt32("id");
            fournisseur.Libelle = reader.GetString("libelle");
            fournisseur.Adresse = reader.GetString("adresse");
            fournisseur.Mail = reader.GetString("mail");
            fournisseur.Telephone = reader.GetString("numero");
            fournisseurs.Add(fournisseur);
        }

        reader.Close();
        return fournisseurs;
    }

    public Fournisseur? GetFournisseur(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelle, adresse, mail, numero from fournisseur where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Fournisseur fournisseur = new Fournisseur();
        while (reader.Read())
        {
            fournisseur.Id = id;
            fournisseur.Libelle = reader.GetString("libelle");
            fournisseur.Adresse = reader.GetString("adresse");
            fournisseur.Mail = reader.GetString("mail");
            fournisseur.Telephone = reader.GetString("numero");
        }

        reader.Close();
        return fournisseur;
    }

    public void AjouterFournisseur(string libelle, string adresse, string mail, string numero)
    {
        MySqlCommand command =
            new MySqlCommand(
                "insert into fournisseur(libelle, adresse, mail, numero) values(@libelle, @adresse, @mail, @numero);",
                _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.Parameters.AddWithValue("adresse", adresse);
        command.Parameters.AddWithValue("mail", mail);
        command.Parameters.AddWithValue("numero", numero);
        command.ExecuteNonQuery();
    }

    public void SupprimerFournisseur(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from fournisseur where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public void ModifierFournisseur(Fournisseur fournisseur)
    {
        MySqlCommand command = new MySqlCommand("update fournisseur set libelle = @libelle, adresse = @adresse, mail = @mail, numero = @numero where id = @id;", _connection);
        command.Parameters.AddWithValue("id", fournisseur.Id);
        command.Parameters.AddWithValue("libelle", fournisseur.Libelle);
        command.Parameters.AddWithValue("adresse", fournisseur.Adresse);
        command.Parameters.AddWithValue("mail", fournisseur.Mail);
        command.Parameters.AddWithValue("numero", fournisseur.Telephone);
        command.ExecuteNonQuery();
    }

    public void AjouterInconnu()
    {
        MySqlCommand command =
            new MySqlCommand(
                "insert into fournisseur(id, libelle, adresse, mail, numero) values(@id, @libelle, @adresse, @mail, @numero);",
                _connection);
        command.Parameters.AddWithValue("id", 1);
        command.Parameters.AddWithValue("libelle", "Inconnu");
        command.Parameters.AddWithValue("adresse", "Inconnue");
        command.Parameters.AddWithValue("mail", "Inconnu");
        command.Parameters.AddWithValue("numero", "Inconnu");
        command.ExecuteNonQuery();
    }
}


public class FournisseurFacadeImpl : IFournisseurFacade
{
    private readonly IFournisseurDao _fournisseurDao;

    public FournisseurFacadeImpl(IFournisseurDao fournisseurDao)
    {
        _fournisseurDao = fournisseurDao;
    }

    public List<Fournisseur> GetFournisseurs()
    {
        return _fournisseurDao.GetFournisseurs();
    }

    public List<Fournisseur> GetFournisseursSansInconnu()
    {
        return _fournisseurDao.GetFournisseursSansInconnu();
    }

    public Fournisseur? GetFournisseur(int id)
    {
        return _fournisseurDao.GetFournisseur(id);
    }

    public void AjouterFournisseur(string libelle, string adresse, string mail, string numero)
    {
        _fournisseurDao.AjouterFournisseur(libelle, adresse, mail, numero);
    }

    public void SupprimerFournisseur(int id)
    {
        _fournisseurDao.SupprimerFournisseur(id);
    }

    public void ModifierFournisseur(Fournisseur fournisseur)
    {
        _fournisseurDao.ModifierFournisseur(fournisseur);
    }

    public void AjouterInconnu()
    {
        _fournisseurDao.AjouterInconnu();
    }

    public Fournisseur GetInconnu()
    {
        Fournisseur? inconnu = GetFournisseur(1);
        if (inconnu != null)
        {
            return inconnu;
        }
        AjouterInconnu();
        inconnu = GetFournisseur(1) ?? throw new Exception(String.Format(MessageUtils.Erreur_Inconnu, "Fournisseur"));
        return inconnu;
    }
}