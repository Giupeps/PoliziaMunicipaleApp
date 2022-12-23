using PoliziaMunicipaleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipaleApp
{
    public class Violazione
    {
        public int Idviolazione { get; set; }
        public string Descrizione { get; set; }

        public static List<Violazione> ListaViolazioni = new List<Violazione>();

        public static void GetViolazione()
        {
            SqlConnection con = Connection.GetConnection();

            try
            {
                con.Open();
                SqlDataReader reader = Connection.GetReader("Select * from VIOLAZIONE", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Violazione v = new Violazione();
                        v.Idviolazione = Convert.ToInt32(reader["Idviolazione"]);
                        v.Descrizione = reader["descrizione"].ToString();
                        ListaViolazioni.Add(v);
                    }
                }
            }
            catch (Exception)
            {
                con.Close();
            }
            con.Close();
        }

        public static void CreaViolazione(Violazione v) 
        {
            SqlConnection con = Connection.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Descrizione", v.Descrizione);

                command.CommandText = "Insert into VIOLAZIONE values (@Descrizione)";
                command.Connection = con;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                con.Close() ;
            }
            con.Close();
        }

        public static List<SelectListItem> DropdownViolazioni = new List<SelectListItem>();

        public static void GetViolazioneDropdown()
        {
            SqlConnection con = Connection.GetConnection();

            try
            {
                con.Open();
                SqlDataReader reader = Connection.GetReader("Select * from Violazione", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem vd = new SelectListItem();                      
                        vd.Text = reader["Descrizione"].ToString();
                        vd.Value = reader["Idviolazione"].ToString();                        
                        DropdownViolazioni.Add(vd);
                    }
                }

            }
            catch (Exception)
            {
                con.Close();
            }
            con.Close();
        }

    }

   
}