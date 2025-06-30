using System.Data.SqlClient;
namespace hastaTakipSistemi
{
    internal class FrmSqlBaglantisi
    {
        string adres = @"Data Source=.\SQLEXPRESS;Initial Catalog=db_HastaneYönetim;Integrated Security=True;Encrypt=False;";
        public SqlConnection baglan()
        {
            SqlConnection baglanti= new SqlConnection(adres);
            baglanti.Open();
            return baglanti;

        }
    }
}
