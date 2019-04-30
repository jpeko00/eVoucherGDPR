using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        
      
    // GET api/values
    public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        //metoda koja se poziva pri unosu novog aliasa (kupac)
        public string Get(string broj, string alias1)
        {
            string error = " ";
            string alias = alias1;
            bool postoji = false;
            int rows = 0;

            if (alias1 == null) // ako je alias prazan, generiraj alias od 5 random slova i 3 random broja
            {
                alias = alias + NovihPetSlova();
                alias = alias + NovaTriBroja();
            }
            else
            {
                do // provjeri postoji li alias u bazi, ako postoji, dodaj mu tri random broja pa provjeri ponovno
                {
                    string rowsno = Get(alias);
                    if (rowsno != "0")
                    {
                        postoji = true;
                        alias = alias + NovaTriBroja();
                    }
                    else
                        postoji = false;
                } while (postoji);
            }

            //ako u bazi postoji broj koji zahtjeva novi alias, updateaj mu alias, ako ne postoji, unesi novi redak u bazu
            try
            {
                builder = getBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT COUNT(*) FROM dbo.ev WHERE number_='"+broj+"'; SELECT @@ROWCOUNT;");
                    string sql = sb.ToString();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    try
                    {
                       rows = (int)cmd.ExecuteScalar();
                       if (rows == 0)
                        {
                            sb.Clear();
                            sb.Append("INSERT INTO dbo.ev (username_, amount_, number_) VALUES ('"+alias+"', 0, '"+broj+"');");
                            SqlCommand cmd2 = new SqlCommand(sb.ToString(),connection);
                            cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            sb.Clear();
                            sb.Append("UPDATE dbo.ev SET username_='"+alias+"' WHERE number_='"+broj+"';");
                            SqlCommand cmd2 = new SqlCommand(sb.ToString(), connection);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        return error = "Dogodila se pogreška prilikom unosa podataka u bazu.";
                    }
                }
            }
            catch (SqlException e)
            {
                return error = "Dogodila se pogreška prilikom povezivanja na server.";
            }
            return alias; // vrati korisniku alias kojeg može koristiti
        }

        //metoda koja poziva proceduru koja provjerava postoji li neki alias u bazi
        //ovu metodu poziva prodavač ako ručno unosi alias, i prethodna metoda u provjeri
        //metoda vraća broj redaka koji odgovaraju upitu
        public String Get(string alias)
        {
            String rows = " ";
            String error = " ";
            
            try
            {
                builder = getBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DECLARE @rows int; EXEC dbo.is_any @alias='"+alias+"', @row_count = @rows OUTPUT; select @rows;");
                    string sql = sb.ToString();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    try
                    {
                        rows = cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            catch (SqlException e)
            {
                error = e.ToString();
            }
            return rows;
        }

        //metoda koja u builder povezuje stringove potrebni za povezivanje
        public SqlConnectionStringBuilder getBuilder()
        {
            builder.DataSource = "evouchergdpr.database.windows.net";
            builder.UserID = "jpeko00";
            builder.Password = "Kj73096L";
            builder.InitialCatalog = "eVoucherDB";

            return builder;
        }

        //metoda koja vraća string s tri random broja
        public string NovaTriBroja()
        {
            String str = "";
            Random _random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int num = _random.Next(0, 9);
                str = str + num;
            }
            return str;
        }

        //metoda koja vraća string s 5 random slova
        public string NovihPetSlova()
        {
            String str = "";
            Random _random = new Random();

            for (int i = 0; i < 5; i++)
            {
                int num = _random.Next(0, 26);
                char let = (char)('a' + num);
                str = str + let;
            }
            return str;
        }

        //metoda koja poziva proceduru za unos nove transakcije u tablicu dbo.trans
        public int[] Get(string alias, string number, int amount)
        {
            int[] tran_id_and_message = { 0, 0 };
            int tran_id = 0;

            try
            {
                builder = getBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DECLARE @code int; EXEC dbo.add_trans @user='" + alias + "', @number='" + number + "', @amount_added=" + amount + ", @state = @code OUTPUT; select @code;");
                    string sql = sb.ToString();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    try
                    {
                        tran_id = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        tran_id_and_message[1] = 1;
                    }
                }
            }
            catch (SqlException e)
            {
                tran_id_and_message[1] = 2;
                return tran_id_and_message;
            }

            if (tran_id != 1 && tran_id != 0)
            {
                tran_id_and_message[0] = tran_id;
                tran_id_and_message[1] = 3;
            }
            else
                tran_id_and_message[1] = 4;

            return tran_id_and_message;
        }
    }
}
