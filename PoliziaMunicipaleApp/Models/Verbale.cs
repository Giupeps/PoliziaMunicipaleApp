using PoliziaMunicipaleApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Punti Decurtati")]
        public int DecurtamentoPunti { get; set; }

        public Violazione Idviolazione { get; set; }
        public Trasgressore IdAnagrafica { get; set; }

        [Display(Name = "Totale Verbali")]
        public int Totale { get; set; }



        public static void CreaVerbale(Verbale ve, int id)
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
                command.Parameters.AddWithValue("@IdAnagrafica", id);
                command.Parameters.AddWithValue("@IdViolazione", ve.Idviolazione.Idviolazione);

                command.CommandText = ("Insert into VERBALE values(@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtazionePunti, @IdAnagrafica, @IdViolazione)");

                command.Connection = con;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                con.Close();
            }
            con.Close();
        }

        public static List<Verbale> ListaVerbTrasgr()
        {
            SqlConnection con = Connection.GetConnection();
            List<Verbale> TotaleXTrasgr = new List<Verbale>();

            try
            {


                con.Open();

                SqlDataReader reader = Connection.GetReader("select Cognome, Nome, VERBALE.Idanagrafica, count(*) as Totale from VERBALE inner join ANAGRAFICA on VERBALE.Idanagrafica = ANAGRAFICA.IDAnagrafica group by VERBALE.Idanagrafica, Cognome, Nome", con);



                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Verbale ve = new Verbale();
                        ve.Totale = Convert.ToInt32(reader["Totale"]);

                        Trasgressore t = new Trasgressore();
                        ve.IdAnagrafica = t;
                        ve.IdAnagrafica.Cognome = reader["Cognome"].ToString();
                        ve.IdAnagrafica.Nome = reader["Nome"].ToString();

                        TotaleXTrasgr.Add(ve);

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return TotaleXTrasgr;
        }

        public static List<Verbale> ListaPtTrasgr()
        {
            SqlConnection con = Connection.GetConnection();
            List<Verbale> PointXTrasgr = new List<Verbale>();

            try
            {
                con.Open();
                SqlDataReader reader = Connection.GetReader("select Cognome, Nome, sum (VERBALE.DecurtamentoPunti) as [Punti Decurtati] from VERBALE inner join ANAGRAFICA on VERBALE.Idanagrafica = ANAGRAFICA.IDAnagrafica group by Cognome, Nome", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Verbale ve = new Verbale();
                        ve.DecurtamentoPunti = Convert.ToInt32(reader["Punti Decurtati"]);

                        Trasgressore t = new Trasgressore();
                        ve.IdAnagrafica = t;
                        ve.IdAnagrafica.Cognome = reader["Cognome"].ToString();
                        ve.IdAnagrafica.Nome = reader["Nome"].ToString();

                        PointXTrasgr.Add(ve);

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return PointXTrasgr;
        }

        public static List<Verbale> ListaOverTenPoints()
        {
            SqlConnection con = Connection.GetConnection();
            List<Verbale> OverTenPoints = new List<Verbale>();

            try
            {
                con.Open();
                SqlDataReader reader = Connection.GetReader("Select Cognome, Nome, sum (VERBALE.DecurtamentoPunti) as [Punti Decurtati] from VERBALE inner join ANAGRAFICA on VERBALE.Idanagrafica = ANAGRAFICA.IDAnagrafica group by Cognome, Nome, VERBALE.DecurtamentoPunti having DecurtamentoPunti >= 10 ", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Verbale ve = new Verbale();
                        ve.DecurtamentoPunti = Convert.ToInt32(reader["Punti Decurtati"]);

                        Trasgressore t = new Trasgressore();
                        ve.IdAnagrafica = t;
                        ve.IdAnagrafica.Cognome = reader["Cognome"].ToString();
                        ve.IdAnagrafica.Nome = reader["Nome"].ToString();

                        OverTenPoints.Add(ve);

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return OverTenPoints;

        }

        public static List<Verbale> ListaCostoMaggiore() 
        { 

            SqlConnection con = Connection.GetConnection();
            List<Verbale> CostoMaggiore = new List<Verbale>();

            try
            {
                con.Open();
                SqlDataReader reader = Connection.GetReader("select Cognome, Nome, VIOLAZIONE.descrizione, VERBALE.Importo from VERBALE left join ANAGRAFICA on VERBALE.Idanagrafica = ANAGRAFICA.IDAnagrafica right join VIOLAZIONE on VERBALE.Idviolazione = VIOLAZIONE.Idviolazione group by Cognome, Nome,VIOLAZIONE.descrizione, VERBALE.Importo having Importo > 400", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Verbale ve = new Verbale();
                        ve.Importo = Convert.ToInt32(reader["Importo"]);

                        Violazione v = new Violazione();
                        ve.Idviolazione = v;
                        v.Descrizione = reader["Descrizione"].ToString();

                        Trasgressore t = new Trasgressore();
                        ve.IdAnagrafica = t;
                        ve.IdAnagrafica.Cognome = reader["Cognome"].ToString();
                        ve.IdAnagrafica.Nome = reader["Nome"].ToString();

                        

                        CostoMaggiore.Add(ve);

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return CostoMaggiore;
        }
    }
}