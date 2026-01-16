using MySql.Data.MySqlClient;

namespace Breeder;

public class Veterinaire
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Mail { get; set; }
    public string Telephone { get; set; }
    public string Adresse { get; set; }

    public override string ToString()
    {
        return Nom;
    }
    
    public override bool Equals(object obj) => Equals(obj as Veterinaire);

    public bool Equals(Veterinaire? statut)
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

    public static bool operator ==(Veterinaire? lhs, Veterinaire? rhs)
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

    public static bool operator !=(Veterinaire? lhs, Veterinaire? rhs) => !(lhs == rhs);
}

public interface IVeterinaireDao
{
    List<Veterinaire> getVeterinaires();
    Veterinaire? GetVeterinaire(int id);
    void AjouterVeterinaire(string nom, string mail, string numero, string adresse);
    void SupprimerVeterinaire(int id);
    void ModifierVeterinaire(Veterinaire veterinaire);
}

public interface IVeterinaireFacade
{
    List<Veterinaire> getVeterinaires();
    Veterinaire? GetVeterinaire(int id);
    void AjouterVeterinaire(string nom, string mail, string numero, string adresse);
    void SupprimerVeterinaire(int id);
    void ModifierVeterinaire(Veterinaire veterinaire);
}


public class VeterinaireDaoImpl : IVeterinaireDao
{
    private readonly MySqlConnection _connection;

    public VeterinaireDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public List<Veterinaire> getVeterinaires()
    {
        MySqlCommand command = new MySqlCommand("select id, nom, mail, numero, adresse from veterinaire;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Veterinaire> veterinaires = new List<Veterinaire>();
        while (reader.Read())
        { 
            Veterinaire veterinaire = new Veterinaire();
            veterinaire.Id = reader.GetInt32("id");
            veterinaire.Nom = reader.GetString("nom");
            veterinaire.Mail = reader.GetString("mail");
            veterinaire.Telephone = reader.GetString("numero");
            veterinaire.Adresse = reader.GetString("adresse");
            veterinaires.Add(veterinaire);
        }

        reader.Close();
        return veterinaires;
    }

    public Veterinaire? GetVeterinaire(int id)
    {
        MySqlCommand command = new MySqlCommand("select nom, mail, numero, adresse from veterinaire where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        
        Veterinaire veterinaire = new Veterinaire();
        while (reader.Read())
        {
            veterinaire.Id = id;
            veterinaire.Nom = reader.GetString("nom");
            veterinaire.Mail = reader.GetString("mail");
            veterinaire.Telephone = reader.GetString("numero");
            veterinaire.Adresse = reader.GetString("adresse");
        }

        reader.Close();
        return veterinaire;
    }

    public void AjouterVeterinaire(string nom, string mail, string numero, string adresse)
    {
        MySqlCommand command = new MySqlCommand("insert into veterinaire(nom, mail, numero, adresse) values(@nom, @mail, @numero, @adresse);", _connection);
        command.Parameters.AddWithValue("nom", nom);
        command.Parameters.AddWithValue("mail", mail);
        command.Parameters.AddWithValue("numero", numero);
        command.Parameters.AddWithValue("adresse", adresse);
        command.ExecuteNonQuery();
    }

    public void SupprimerVeterinaire(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from veterinaire where id = @id", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public void ModifierVeterinaire(Veterinaire veterinaire)
    {
        MySqlCommand command = new MySqlCommand("update veterinaire set nom = @nom, mail = @mail, numero = @numero, adresse = @adresse where id = @id;", _connection);
        command.Parameters.AddWithValue("id", veterinaire.Id);
        command.Parameters.AddWithValue("nom", veterinaire.Nom);
        command.Parameters.AddWithValue("mail", veterinaire.Mail);
        command.Parameters.AddWithValue("numero", veterinaire.Telephone);
        command.Parameters.AddWithValue("adresse", veterinaire.Adresse);
        command.ExecuteNonQuery();
    }
}


public class VeterinaireFacadeImpl : IVeterinaireFacade
{
    private readonly IVeterinaireDao _veterinaireDao;

    public VeterinaireFacadeImpl(IVeterinaireDao veterinaireDao)
    {
        _veterinaireDao = veterinaireDao;
    }
    
    public List<Veterinaire> getVeterinaires()
    {
        return _veterinaireDao.getVeterinaires();
    }

    public Veterinaire? GetVeterinaire(int id)
    {
        return _veterinaireDao.GetVeterinaire(id);
    }

    public void AjouterVeterinaire(string nom, string mail, string numero, string adresse)
    {
        _veterinaireDao.AjouterVeterinaire(nom, mail, numero, adresse);
    }

    public void SupprimerVeterinaire(int id)
    {
        _veterinaireDao.SupprimerVeterinaire(id);
    }

    public void ModifierVeterinaire(Veterinaire veterinaire)
    {
        _veterinaireDao.ModifierVeterinaire(veterinaire);
    }
}