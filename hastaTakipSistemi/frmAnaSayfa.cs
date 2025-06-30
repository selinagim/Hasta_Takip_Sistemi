using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastaTakipSistemi
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }
        FrmSqlBaglantisi bgl = new FrmSqlBaglantisi();
        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            Listele();
            durumDoldur();
            bolumDoldur();
        }
        private void Listele()
        {
            SqlCommand liste = new SqlCommand("listele", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(liste);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;



        }
        private void durumDoldur()
        {
            SqlCommand durum = new SqlCommand("durumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(durum);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDurum.DataSource = dt;
            cmbDurum.DisplayMember = "durumAd";
            cmbDurum.ValueMember = "durumID";
        }

        private void bolumDoldur()
        {
            SqlCommand bolum = new SqlCommand("bolumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(bolum);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBolum.DataSource = dt;
            cmbBolum.DisplayMember = "bolumAd";
            cmbBolum.ValueMember = "bolumID";

        }






        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtTc.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtTelefon.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtYas.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtCinsiyet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            txtTarih.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            cmbDurum.SelectedValue = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            cmbBolum.SelectedValue = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
           
            bool IsEx = Convert.ToBoolean(dataGridView1.Rows[secilen]. Cells[11]. Value);
            if (IsEx)
            {
                rbEvet.Checked = true;
            }
            else
            {
                rbHayir.Checked = true;


            }





        }

        private void rbEvet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEvet.Checked == true)
            {
                lblEx.Text = "True";

            }
            else
            {
                lblEx.Text = "False";
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text != "" && txtSoyad.Text != "" && txtCinsiyet.Text != "" && txtTc.Text != "" && txtSikayet.Text != ""  && txtYas.Text != "" && cmbBolum.Text != "" && cmbDurum.Text != "") 
            {
                kaydet();
            }
            else
            {
                MessageBox.Show("Lütfen ilgili Tüm Alanları Doldurunuz!", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void kaydet()
        {
            SqlCommand kaydet = new SqlCommand("kaydet",bgl.baglan());
            kaydet.CommandType = CommandType.StoredProcedure;
            kaydet.Parameters.AddWithValue("ad",txtAd.Text);
            kaydet.Parameters.AddWithValue("soyad",txtSoyad.Text);
            kaydet.Parameters.AddWithValue("tc", txtTc.Text);
            kaydet.Parameters.AddWithValue("tarih",DateTime.Now);
            kaydet.Parameters.AddWithValue("sikayet", txtSikayet.Text);
            kaydet.Parameters.AddWithValue("tel", txtTelefon.Text);
            kaydet.Parameters.AddWithValue("cins", txtCinsiyet.Text);
            kaydet.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));
  
            kaydet.Parameters.AddWithValue("durum",cmbDurum.SelectedValue);
            kaydet.Parameters.AddWithValue("bolum", cmbBolum.SelectedValue);
            if (lblEx.Text=="True")
            {
                kaydet.Parameters.AddWithValue("ex",1);

            }
            else
            {
                kaydet.Parameters.AddWithValue("ex", 0);
            }
            kaydet.ExecuteNonQuery();

            MessageBox.Show("Kayıt Başarıyla Eklendi", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            



        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            sil();
        }
        private void sil()
        {
            DialogResult dr=MessageBox.Show($"{txtID.Text} numaralı kayıt silinecek. Onaylıyor musunuz?","Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                SqlCommand sil = new SqlCommand("sil", bgl.baglan());
                sil.CommandType = CommandType.StoredProcedure;
                sil.Parameters.AddWithValue("id", int.Parse(txtID.Text));
                sil.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarıyla Silindi", "Kayıt Silme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show($"{txtID.Text} numaralı kayıt güncellenecek. Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                guncelle();
            }
        }
        private void guncelle()
        {
            SqlCommand guncelle = new SqlCommand("guncelle", bgl.baglan());
            guncelle.CommandType = CommandType.StoredProcedure;
            guncelle.Parameters.AddWithValue("id", int.Parse(txtID.Text));
            guncelle.Parameters.AddWithValue("ad", txtAd.Text);
            guncelle.Parameters.AddWithValue("soyad", txtSoyad.Text);
            guncelle.Parameters.AddWithValue("tc", txtTc.Text);
            guncelle.Parameters.AddWithValue("tarih", DateTime.Now);
            guncelle.Parameters.AddWithValue("sikayet", txtSikayet.Text);
            guncelle.Parameters.AddWithValue("tel", txtTelefon.Text);
            guncelle.Parameters.AddWithValue("cins", txtCinsiyet.Text);
            guncelle.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));

            guncelle.Parameters.AddWithValue("durum", cmbDurum.SelectedValue);
            guncelle.Parameters.AddWithValue("bolum", cmbBolum.SelectedValue);
            if (lblEx.Text == "True")
            {
                guncelle.Parameters.AddWithValue("ex", 1);

            }
            else
            {
                guncelle.Parameters.AddWithValue("ex", 0);
            }
            guncelle.ExecuteNonQuery();

            MessageBox.Show("Güncelleme İşlemi Başarılı", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
        private void temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtCinsiyet.Text = "";
            txtTc.Text = "";
            txtTelefon.Text = "";
            cmbBolum.Text = "";
            cmbDurum.Text = "";
            txtYas.Text = "";
            txtID.Text = "";
            txtSikayet.Text = "";
            txtTarih.Text = "";
            rbHayir.Checked = true;
            lblEx.Text = "false";


        }

        private void btnFormuTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnİstatistikler_Click(object sender, EventArgs e)
        {
            frmIstatistic fr=new frmIstatistic();
            fr.Show();
        }
    }
}
    
