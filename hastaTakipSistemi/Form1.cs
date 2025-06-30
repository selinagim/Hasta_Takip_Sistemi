using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hastaTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         FrmSqlBaglantisi bgl=new FrmSqlBaglantisi();
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            FrmKayit fr = new FrmKayit();
            fr.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if(txtKulAdi.Text != "" && txtSifre.Text != "")
            {
                SqlCommand giris = new SqlCommand("girisYap",bgl.baglan());
                giris.CommandType = CommandType.StoredProcedure;
                giris.Parameters.AddWithValue("kulAdi",txtKulAdi.Text);
                giris.Parameters.AddWithValue("sifre",txtSifre.Text);
                SqlDataReader dr = giris.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş işlemi başarılı", "Giriş işlemi başarılı", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    frmAnaSayfa fr = new frmAnaSayfa();
                    fr.Show();
                   this.Hide();
                }
                else
                {
                    MessageBox.Show("Giriş işlemi başarısız", "Giriş işlemi başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz", "Hata", MessageBoxButtons.OK);
            }
        }
    }
}
