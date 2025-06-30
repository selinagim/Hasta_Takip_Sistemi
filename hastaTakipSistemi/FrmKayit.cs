using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace hastaTakipSistemi
{
    public partial class FrmKayit : Form
    {
        public FrmKayit()
        {
            InitializeComponent();
        }
        FrmSqlBaglantisi bgl=new FrmSqlBaglantisi();

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            if (txtKulAdi.Text != "" && txtSifre.Text != "")
            {
                SqlCommand kayit = new SqlCommand("kayitOl",bgl.baglan());
                kayit.CommandType=CommandType.StoredProcedure;
                kayit.Parameters.AddWithValue("kulAdi",txtKulAdi.Text);
                kayit.Parameters.AddWithValue("sifre", txtSifre.Text);
                kayit.ExecuteNonQuery();
                MessageBox.Show("Kayıt İşlemi başarılı", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 


            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz", "Hata", MessageBoxButtons.OK);
            }
        }
    }
}
