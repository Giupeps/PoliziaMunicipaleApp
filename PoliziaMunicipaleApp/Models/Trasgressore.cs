using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PoliziaMunicipaleApp.Models
{
    public class Trasgressore
    {
        public int IDAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CodiceFiscale { get; set; }

        public static List<Trasgressore> ListaTrasgressori = new List<Trasgressore>();

        public static void GetTrasgressore()
        {
            SqlConnection con = Connection.GetConnection();
            try
            {
                con.Open();
                SqlDataReader reader = Connection.GetReader("Select * from ANAGRAFICA", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Trasgressore t = new Trasgressore();
                        t.IDAnagrafica = Convert.ToInt32(reader["IDAnagrafica"]);
                        t.Cognome = reader["Cognome"].ToString();
                        t.Nome = reader["Nome"].ToString();
                        t.Indirizzo = reader["Indirizzo"].ToString();
                        t.Citta = reader["Citta"].ToString();
                        t.CAP = reader["CAP"].ToString();
                        t.CodiceFiscale = reader["Cod_Fisc"].ToString();
                        ListaTrasgressori.Add(t);
                    }

                }
            }
            catch (Exception)
            {
                con.Close();
            }
            con.Close();
        }
        public static void Create(Trasgressore t)
        {
            SqlConnection con = Connection.GetConnection();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Cognome", t.Cognome);
                command.Parameters.AddWithValue("@Nome", t.Nome);
                command.Parameters.AddWithValue("@Indirizzo", t.Indirizzo);
                command.Parameters.AddWithValue("@Citta", t.Citta);
                command.Parameters.AddWithValue("@CAP", t.CAP);
                command.Parameters.AddWithValue("@Cod_Fisc", t.CodiceFiscale);

                command.CommandText = "Insert into ANAGRAFICA values(@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)";

                command.Connection = con;
                command.ExecuteNonQuery();
              

            }
            catch (Exception)
            {
                con.Close();
            }

            con.Close();
        }
    }
}