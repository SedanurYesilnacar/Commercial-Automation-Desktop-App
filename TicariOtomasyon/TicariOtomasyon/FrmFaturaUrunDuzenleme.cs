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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        public string UrunID;

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunID.Text = UrunID;

            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_FATURADETAY WHERE FATURAURUNID = @P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", UrunID);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                TxtFiyat.Text = dr[3].ToString();
                TxtMiktar.Text = dr[2].ToString();
                TxtTutar.Text = dr[4].ToString();
                TxtUrunAd.Text = dr[1].ToString();
            }

            bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURADETAY SET URUNAD = @P1, MIKTAR = @P2, FIYAT = @P3, TUTAR = @P4 WHERE FATURAURUNID = @P5", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMiktar.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@P5", TxtUrunID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_FATURADETAY WHERE FATURAURUNID = @P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Faturaya ait ürün silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
