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

namespace TicariOtomasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTlf1.Text = "";
            MskTlf2.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            CBEil.Text = "";
            CBEilce.Text = "";
            RchAdres.Text = "";
            TxtVergi.Text = "";
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                CBEil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void ilceListesi()
        {
            CBEilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CBEil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CBEilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirListesi();
        }

        private void CBEilce_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void CBEil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListesi();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_MUSTERILER (AD, SOYAD, TELEFON, TELEFON2, TC, MAIL, IL, ILCE, ADRES, VERGIDAIRE) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTlf1.Text);
            komut.Parameters.AddWithValue("@p4", MskTlf2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", CBEil.Text);
            komut.Parameters.AddWithValue("@p8", CBEilce.Text);
            komut.Parameters.AddWithValue("@p9", RchAdres.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTlf1.Text = dr["TELEFON"].ToString();
                MskTlf2.Text = dr["TELEFON2"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CBEil.Text = dr["IL"].ToString();
                CBEilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kullanıcı silinecek", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
            if(secenek == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("DELETE FROM TBL_MUSTERILER WHERE ID = @p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtId.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Müşteri silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                listele();
                temizle();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_MUSTERILER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, TELEFON2=@P4, TC=@P5, MAIL=@P6, IL=@P7, ILCE=@P8, VERGIDAIRE=@P9, ADRES=@P10 WHERE ID = @P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTlf1.Text);
            komut.Parameters.AddWithValue("@P4", MskTlf2.Text);
            komut.Parameters.AddWithValue("@P5", MskTC.Text);
            komut.Parameters.AddWithValue("@P6", TxtMail.Text);
            komut.Parameters.AddWithValue("@P7", CBEil.Text);
            komut.Parameters.AddWithValue("@P8", CBEilce.Text);
            komut.Parameters.AddWithValue("@P9", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P10", RchAdres.Text);
            komut.Parameters.AddWithValue("@P11", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }
    }
}
