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
    public partial class FrmFirmalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmFirmalar()
        {
            InitializeComponent();
        }

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSektor.Text = "";
            TxtYetkili.Text = "";
            TxtGorev.Text = "";
            MskTC.Text = "";
            MskTlf1.Text = "";
            MskTlf2.Text = "";
            MskTlf3.Text = "";
            MskFax.Text = "";
            TxtMail.Text = "";
            CBEil.Text = "";
            CBEilce.Text = "";
            TxtVergi.Text = "";
            RchAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
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

        void cariKodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void labelControl10_Click(object sender, EventArgs e)
        {
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirListesi();
            cariKodAciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskTC.Text = dr["YETKILITC"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskTlf1.Text = dr["TELEFON1"].ToString();
                MskTlf2.Text = dr["TELEFON2"].ToString();
                MskTlf3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                CBEil.Text = dr["IL"].ToString();
                CBEilce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FIRMALAR (AD, YETKILISTATU, YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON1, TELEFON2, TELEFON3, MAIL, FAX, IL, ILCE, VERGIDAIRE, ADRES, OZELKOD1, OZELKOD2, OZELKOD3) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtGorev.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", MskTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskTlf1.Text);
            komut.Parameters.AddWithValue("@p7", MskTlf2.Text);
            komut.Parameters.AddWithValue("@p8", MskTlf3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", CBEil.Text);
            komut.Parameters.AddWithValue("@p12", CBEilce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergi.Text);
            komut.Parameters.AddWithValue("@p14", RchAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void CBEil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListesi();
        }

        private void RchKod1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_FIRMALAR WHERE ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FIRMALAR SET AD = @P1, YETKILISTATU = @P2, YETKILIADSOYAD = @P3, YETKILITC = @P4, SEKTOR = @P5, TELEFON1 = @P6, TELEFON2 = @P7, TELEFON3 = @P8, MAIL = @P9, FAX = @P10, IL = @P11, ILCE = @P12, VERGIDAIRE = @P13, ADRES = @P14, OZELKOD1 = @P15, OZELKOD2 = @P16, OZELKOD3 = @P17 WHERE ID = @P18", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtGorev.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTlf1.Text);
            komut.Parameters.AddWithValue("@P7", MskTlf2.Text);
            komut.Parameters.AddWithValue("@P8", MskTlf3.Text);
            komut.Parameters.AddWithValue("@P9", TxtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
            komut.Parameters.AddWithValue("@P11", CBEil.Text);
            komut.Parameters.AddWithValue("@P12", CBEilce.Text);
            komut.Parameters.AddWithValue("@P13", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P14", RchAdres.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komut.Parameters.AddWithValue("@P18", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
