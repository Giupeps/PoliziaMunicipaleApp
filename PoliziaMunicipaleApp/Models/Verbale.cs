using PoliziaMunicipaleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PoliziaMunicipaleApp
{
    public class Verbale
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }

        public Violazione Idviolazione { get; set; }
        public Trasgressore IdAnagrafica { get; set; }

        public static void CreaVerbale(Verbale ve)
        {
            SqlConnection con = Connection.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@DataViolazione", ve.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", ve.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NominativoAgente", ve.NominativoAgente);
                command.Parameters.AddWithValue("DataTrascrizioneVerbale", ve.DataTrascrizioneVerbale);
                command.Parameters.AddWithValue("@Importo", ve.Importo);
                command.Parameters.AddWithValue("@DecurtazionePunti", ve.DecurtamentoPunti);
                command.Parameters.AddWithValue("@IdAnagrafica", ve.IdAnagrafica);
                command.Parameters.AddWithValue("@IdViolazione", ve.Idviolazione);
            }
            catch (Exception)
            {
                con.Close();
            }
            con.Close();
        }
    }
}