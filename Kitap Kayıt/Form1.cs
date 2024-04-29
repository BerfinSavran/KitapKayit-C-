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
using System.Diagnostics;

namespace Kitap_Kayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin\SQLEXPRESS;Initial Catalog=KitapVt;Integrated Security=True");
        
        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select * from TblKitaplar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        void Turler()
        {
            SqlCommand komut = new SqlCommand("Select * from TblTurler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbTur.ValueMember = "Turid";
            CmbTur.DisplayMember = "TurAd";
            CmbTur.DataSource = dt;
            CmbTur.SelectedIndex = 0;
        }
        
        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
            Turler();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutekle = new SqlCommand("insert into TblKitaplar(KitapAd,Yazar,Sayfa,Fiyat,Yayınevi,Tur) values(@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komutekle.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
            komutekle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komutekle.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komutekle.Parameters.AddWithValue("@p4", TxtFiyat.Text);
            komutekle.Parameters.AddWithValue("@p5", TxtYayinevi.Text);
            komutekle.Parameters.AddWithValue("@p6", CmbTur.SelectedIndex+1);
            
            komutekle.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kitap Veri Tabanına Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKitapid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKitapAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtYayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand Komutsil = new SqlCommand("Delete From TblKitaplar where Kitapid = @p1", baglanti);
            Komutsil.Parameters.AddWithValue("@p1", TxtKitapid.Text);
            Komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Veri Tabanından Silindi", "Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update TblKitaplar set KitapAd=@p1, Yazar=@p2, Sayfa=@p3, Fiyat=@p4, Yayınevi=@p5, Tur=@p6 where Kitapid=@p7", baglanti);
            komutguncelle.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komutguncelle.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komutguncelle.Parameters.AddWithValue("@p4", TxtFiyat.Text);
            komutguncelle.Parameters.AddWithValue("@p5", TxtYayinevi.Text);
            komutguncelle.Parameters.AddWithValue("@p6", CmbTur.SelectedIndex + 1);
            komutguncelle.Parameters.AddWithValue("@p7", TxtKitapid.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Listele();
        }
    }
}
//Data Source=Berfin\SQLEXPRESS;Initial Catalog=KitapVt;Integrated Security=True;Trust Server Certificate=True
