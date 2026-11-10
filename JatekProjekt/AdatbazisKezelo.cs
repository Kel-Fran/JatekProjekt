using MySql.Data.MySqlClient;
namespace JatekProjekt
{

    public struct AdatbazisKezelo
    {
        private const string k_ConnectionString = "server=localhost;user=root;password=pwd;database=JatekDb";

        /// Beolvassa az összes karaktert az adatbázisból egy List<Karakter>-be.
       
        public List<Karakter> AdatokBetoltese()
        {
            var karakterek = new List<Karakter>();

            using var connection = new MySqlConnection(k_ConnectionString);
            connection.Open();

            using var cmd = new MySqlCommand("SELECT * FROM Karakterek", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read()) karakterek.Add(new(
                                                     Id      : Convert.ToInt32(reader["Id"]),
                                                     Nev     : reader["Nev"].ToString(),
                                                     Osztaly : reader["Osztaly"].ToString(),
                                                     Szint   : Convert.ToInt32(reader["Szint"]),
                                                     Arany   : Convert.ToInt32(reader["Arany"]),
                                                     Szerver : reader["Szerver"].ToString()
                                                 ));

            return karakterek;
        }
    }
}
