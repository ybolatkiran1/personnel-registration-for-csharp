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

namespace personelkayit
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AN0V9P0\\MSSQLSERVER01;Initial Catalog=PersonelVeriTabani;Integrated Security=True");
        //önemli
        void temizle()
        {
            Txtİd.Text = "";
            TxtAd.Text = "";
            TxtMeslek.Text = "";
            TxtSoyad.Text = "";
            maskMaas.Text = "";
            cmbSehir.Text = "";
            radiobekar.Checked=false;
            radioevli.Checked = false;
            TxtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet1.Tbl_Personel);

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti); //önemli
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4",maskMaas.Text);
            komut.Parameters.AddWithValue("@p5",TxtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", label8.Text);
            komut.ExecuteNonQuery(); //sorgu çalıştırır



            baglanti.Close();
            MessageBox.Show("Personel Eklendi");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet1.Tbl_Personel);
        }

        private void radioevli_CheckedChanged(object sender, EventArgs e)
        {
            if (radioevli.Checked == true) { label8.Text = "True"; }
        }

        private void radiobekar_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobekar.Checked == true) { label8.Text = "False"; }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; //herhangi birine çift tıklayınca seçilen değere aktarır
            Txtİd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbSehir.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskMaas.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtMeslek.Text= dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioevli.Checked = true;
            }
            if(label8.Text == "False")
            {
                radiobekar.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel Where PerId=@k1", baglanti);
            komutsil.Parameters.AddWithValue("@k1", Txtİd.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");

        }

        private void btnGüncel_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1,PerSoyad=@a2,PerSehir=@a3,PerMaas=@a4,PerDurum=@a5,PerMeslek=@a6 where PerId=@a7", baglanti);
            komutguncelle.Parameters.AddWithValue("@a1", TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@a2",TxtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", cmbSehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4",maskMaas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label8.Text);
            komutguncelle.Parameters.AddWithValue("@a6", TxtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7",Txtİd.Text);
            komutguncelle.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Personel Listesi Güncellendi!");
        }

        private void btnİstatistik_Click(object sender, EventArgs e)
        {
            Frmİstatistik fr = new Frmİstatistik();
            fr.Show();
            
        }

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            FrmGrafikler fr1=new FrmGrafikler();
            fr1.Show();
        }

        private void kodlayıcıHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu Program Ybolatkiran Tarafından Kodlanmış olup tüm hakları saklıdır!", "CODER BY YBOLATKIRAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void girişEkranınaDönToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiris frm= new FrmGiris();
            frm.Show();
            this.Close();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
