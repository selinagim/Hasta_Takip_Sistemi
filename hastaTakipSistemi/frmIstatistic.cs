using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastaTakipSistemi
{
    public partial class frmIstatistic : Form
    {
        public frmIstatistic()
        {
            InitializeComponent();
        }
        FrmSqlBaglantisi bgl=new FrmSqlBaglantisi();
        private void frmIstatistic_Load(object sender, EventArgs e)
        {
            toplamHasta();
            yasOrtalama();
            erkekSayi();
            kadinSayi();
            ExSayi();
        }
        private void toplamHasta()
        {
            SqlCommand toplam= new SqlCommand("select count (*) from tblHastaBilgi",bgl.baglan());
            SqlDataReader dr=toplam.ExecuteReader();
            while (dr.Read())
            {
                lblToplamHasta.Text = dr[0].ToString();
            }

        }
        private void yasOrtalama()
        {
            SqlCommand toplam = new SqlCommand("select avg(hYas) from tblHastaBilgi", bgl.baglan());
            SqlDataReader dr = toplam.ExecuteReader();
            while (dr.Read())
            {
                lblYasOrtalama.Text = dr[0].ToString();
            }

        }
        private void erkekSayi()
        {
            SqlCommand toplam = new SqlCommand("select count(*) from tblHastaBilgi where hCinsiyet='Erkek'", bgl.baglan());
            SqlDataReader dr = toplam.ExecuteReader();
            while (dr.Read())
            {
                lblErkekSayi.Text = dr[0].ToString();
            }

        }
        private void kadinSayi()
        {
            SqlCommand toplam = new SqlCommand("select count(*) from tblHastaBilgi where hCinsiyet='Kadın'", bgl.baglan());
            SqlDataReader dr = toplam.ExecuteReader();
            while (dr.Read())
            {
                lblKadinSayi.Text = dr[0].ToString();
            }

        }
        private void ExSayi()
        {
            SqlCommand toplam = new SqlCommand("select count(*) from tblHastaBilgi where hExMi=1", bgl.baglan());
            SqlDataReader dr = toplam.ExecuteReader();
            while (dr.Read())
            {
                lblExSayi.Text = dr[0].ToString();
            }

        }
    }
}
