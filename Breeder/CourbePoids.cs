using MySql.Data.MySqlClient;

namespace Breeder;

public class CourbePoids {
    public int IdAnimal { get; set; }
    public DateTime DateSaisie { get; set; }
    public decimal Poids { get; set; }
    public decimal Moyenne { get; set; }

    public override bool Equals(object obj) => Equals(obj as CourbePoids);

    public bool Equals(CourbePoids? statut) {
        if (statut is null) {
            return false;
        }

        // Optimization for a common success case.
        if (ReferenceEquals(this, statut)) {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (GetType() != statut.GetType()) {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return IdAnimal == statut.IdAnimal && DateSaisie == statut.DateSaisie;
    }

    public override int GetHashCode() => (IdAnimal, DateSaisie).GetHashCode();

    public static bool operator ==(CourbePoids? lhs, CourbePoids? rhs) {
        if (lhs is null) {
            if (rhs is null) {
                return true;
            }

            // Only the left side is null.
            return false;
        }

        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(CourbePoids? lhs, CourbePoids? rhs) => !(lhs == rhs);
}

public interface IPoidsDao {
    void ModifierTempsPeriodeObservation(int jours);
    int GetTempsPeriodeObservation();

    void AjouterPoids(int idAnimal, decimal poids);
    decimal GetPoids(int idAnimal, DateTime dateSaisie);

    List<CourbePoids> GetCourbesPoids(int idAnimal);
}

public interface IPoidsFacade {
    void ModifierTempsPeriodeObservation(int jours);
    int GetTempsPeriodeObservation();

    void AjouterPoids(int idAnimal, decimal poids);
    decimal GetPoids(int idAnimal, DateTime dateSaisie);

    List<CourbePoids> GetCourbesPoids(int idAnimal);
    decimal GetMoyennePoids(int idAnimal);
    bool VerifierSaisiePoidsAujourdhui(int idAnimal);
}

public class PoidsDaoImpl : IPoidsDao {
    private readonly MySqlConnection _connection;

    public PoidsDaoImpl(MySqlConnection connection) {
        _connection = connection;
    }

    public void ModifierTempsPeriodeObservation(int jours) {
        MySqlCommand command = new MySqlCommand("update periodeobservation set periode = @jours;", _connection);
        command.Parameters.AddWithValue("jours", jours);
        command.ExecuteNonQuery();
    }

    public int GetTempsPeriodeObservation() {
        MySqlCommand command = new MySqlCommand("select periode from periodeobservation;", _connection);
        return (Int32)command.ExecuteScalar();
    }

    public void AjouterPoids(int idAnimal, decimal poids) {
        MySqlCommand command = new MySqlCommand(
            "insert into courbespoids(idAnimal, dateSaisie, poids) values(@idAnimal, @dateSaisie, @poids);",
            _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.Parameters.AddWithValue("dateSaisie", DateTime.Today);
        command.Parameters.AddWithValue("poids", poids);
        command.ExecuteNonQuery();
    }


    public decimal GetPoids(int idAnimal, DateTime dateSaisie) {
        MySqlCommand command =
            new MySqlCommand("select poids from courbespoids where idAnimal = @idAnimal and dateSaisie = @dateSaisie;",
                _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.Parameters.AddWithValue("dateSaisie", dateSaisie);
        return (decimal)command.ExecuteScalar();
    }

    public List<CourbePoids> GetCourbesPoids(int idAnimal) {
        MySqlCommand command =
            new MySqlCommand("select dateSaisie, poids, moyenne from courbespoids where idAnimal = @idAnimal;",
                _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<CourbePoids> courbesPoids = new List<CourbePoids>();
        while (reader.Read()) {
            CourbePoids courbePoids = new CourbePoids();
            courbePoids.IdAnimal = idAnimal;
            courbePoids.DateSaisie = reader.GetDateTime("dateSaisie");
            courbePoids.Poids = reader.GetDecimal("poids");
            courbePoids.Moyenne = reader.GetDecimal("moyenne");
            courbesPoids.Add(courbePoids);
        }

        reader.Close();
        return courbesPoids;
    }

    public List<CourbePoids> GetCourbesPoids() {
        MySqlCommand command =
            new MySqlCommand("select idAnimal, dateSaisie, poids, moyenne from courbespoids", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<CourbePoids> courbesPoids = new List<CourbePoids>();
        while (reader.Read()) {
            CourbePoids courbePoids = new CourbePoids();
            courbePoids.IdAnimal = reader.GetInt32("idAnimal");
            courbePoids.DateSaisie = reader.GetDateTime("dateSaisie");
            courbePoids.Poids = reader.GetDecimal("poids");
            courbePoids.Moyenne = reader.GetDecimal("moyenne");
            courbesPoids.Add(courbePoids);
        }

        reader.Close();
        return courbesPoids;
    }
}

public class PoidsFacadeImpl : IPoidsFacade {
    private readonly IPoidsDao _poidsDao;

    public PoidsFacadeImpl(IPoidsDao poidsDao) {
        _poidsDao = poidsDao;
    }

    public void ModifierTempsPeriodeObservation(int jours) {
        _poidsDao.ModifierTempsPeriodeObservation(jours);
    }

    public int GetTempsPeriodeObservation() {
        return _poidsDao.GetTempsPeriodeObservation();
    }

    public void AjouterPoids(int idAnimal, decimal poids) {
        _poidsDao.AjouterPoids(idAnimal, poids);
    }

    public decimal GetPoids(int idAnimal, DateTime dateSaisie) {
        return _poidsDao.GetPoids(idAnimal, dateSaisie);
    }

    public List<CourbePoids> GetCourbesPoids(int idAnimal) {
        return _poidsDao.GetCourbesPoids(idAnimal);
    }

    public decimal GetMoyennePoids(int idAnimal) {
        return GetCourbesPoids(idAnimal).Average(poids => poids.Poids);
    }

    public bool VerifierSaisiePoidsAujourdhui(int idAnimal) {
        List<CourbePoids> courbesPoids = GetCourbesPoids(idAnimal);
        courbesPoids.Sort((x, y) => x.DateSaisie.CompareTo(y.DateSaisie));

        if (courbesPoids.Count == 0) {
            return false;
        }

        CourbePoids courbePoids = courbesPoids[^1];
        return courbePoids.DateSaisie != DateTime.Today;
    }
}