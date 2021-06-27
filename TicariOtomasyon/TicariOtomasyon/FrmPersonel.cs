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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_PERSONELLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
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

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTlf.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            CBEil.Text = "";
            CBEilce.Text = "";
            TxtGorev.Text = "";
            RchAdres.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirListesi();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_PERSONELLER (AD, SOYAD, TELEFON, TC, MAIL, IL, ILCE, ADRES, GOREV) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTlf.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", CBEil.Text);
            komut.Parameters.AddWithValue("@P7", CBEilce.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel bilgileri kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void CBEil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTlf.Text = dr["TELEFON"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CBEil.Text = dr["IL"].ToString();
                CBEilce.Text = dr["ILCE"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_PERSONELLER WHERE ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_PERSONELLER SET AD = @P1, SOYAD = @P2, TELEFON = @P3, TC = @P4, MAIL = @P5, IL = @P6, ILCE = @P7, ADRES = @P8, GOREV = @P9 WHERE ID = @P10", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTlf.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", CBEil.Text);
            komut.Parameters.AddWithValue("@P7", CBEilce.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komut.Parameters.AddWithValue("@P10", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
