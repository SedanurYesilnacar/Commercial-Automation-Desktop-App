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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FATURABILGI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtUrunID.Text = "";
            TxtUrunAd.Text = "";
            TxtMiktar.Text = "";
            TxtFiyat.Text = "";
            TxtTutar.Text = "";
            TxtFaturaID.Text = "";
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // Firma carisi
            if(TxtFaturaID.Text == "" && comboBox1.Text == "Firma")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURABILGI (SERI, SIRANO, TARIH, SAAT, VERGIDAIRE, ALICI, TESLIMEDEN, TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@P3", MskTarih.Text);
                komut.Parameters.AddWithValue("@P4", MskSaat.Text);
                komut.Parameters.AddWithValue("@P5", TxtVergi.Text);
                komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@P7", TxtTEden.Text);
                komut.Parameters.AddWithValue("@P8", TxtTAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura bilgisi kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if(TxtFaturaID.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("INSERT INTO TBL_FATURADETAY (URUNAD, MIKTAR, FIYAT, TUTAR, FATURAID) VALUES (@P1, @P2, @P3, @P4, @P5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@P2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", TxtFaturaID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("INSERT INTO TBL_FIRMAHAREKETLER (URUNID, ADET, PERSONEL, FIRMA, FIYAT, TOPLAM, FATURAID, TARIH, NOTLAR) VALUES (@H1, @H2, @H3, @H4, @H5, @H6, @H7, @H8, @H9)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@H1", TxtUrunID.Text);
                komut3.Parameters.AddWithValue("@H2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@H3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@H4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@H5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@H6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@H7", TxtFaturaID.Text);
                komut3.Parameters.AddWithValue("@H8", MskTarih.Text);
                komut3.Parameters.AddWithValue("@H9", RchNotlar.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Stok sayısını azaltma
                SqlCommand komut4 = new SqlCommand("UPDATE TBL_URUNLER SET ADET = ADET - @S1 WHERE ID = @S2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@S1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@S2", TxtUrunID.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya ait ürün kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }

            // Müşteri carisi
            if (TxtFaturaID.Text == "" && comboBox1.Text == "Müşteri")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURABILGI (SERI, SIRANO, TARIH, SAAT, VERGIDAIRE, ALICI, TESLIMEDEN, TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@P3", MskTarih.Text);
                komut.Parameters.AddWithValue("@P4", MskSaat.Text);
                komut.Parameters.AddWithValue("@P5", TxtVergi.Text);
                komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@P7", TxtTEden.Text);
                komut.Parameters.AddWithValue("@P8", TxtTAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura bilgisi kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if (TxtFaturaID.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("INSERT INTO TBL_FATURADETAY (URUNAD, MIKTAR, FIYAT, TUTAR, FATURAID) VALUES (@P1, @P2, @P3, @P4, @P5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@P2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", TxtFaturaID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("INSERT INTO TBL_MUSTERIHAREKETLER (URUNID, ADET, PERSONEL, MUSTERI, FIYAT, TOPLAM, FATURAID, TARIH, NOTLAR) VALUES (@H1, @H2, @H3, @H4, @H5, @H6, @H7, @H8, @H9)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@H1", TxtUrunID.Text);
                komut3.Parameters.AddWithValue("@H2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@H3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@H4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@H5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@H6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@H7", TxtFaturaID.Text);
                komut3.Parameters.AddWithValue("@H8", MskTarih.Text);
                komut3.Parameters.AddWithValue("@H9", RchNotlar.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Stok sayısını azaltma
                SqlCommand komut4 = new SqlCommand("UPDATE TBL_URUNLER SET ADET = ADET - @S1 WHERE ID = @S2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@S1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@S2", TxtUrunID.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya ait ürün kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr != null)
            {
                TxtId.Text = dr["FATURABILGIID"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtSiraNo.Text = dr["SIRANO"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTAlan.Text = dr["TESLIMALAN"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_FATURABILGI WHERE FATURABILGIID = @P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura bilgisi silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURABILGI SET SERI = @P1, SIRANO = @P2, TARIH = @P3, SAAT = @P4, VERGIDAIRE = @P5, ALICI = @P6, TESLIMEDEN = @P7, TESLIMALAN = @P8 WHERE FATURABILGIID = @P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtTEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTAlan.Text);
            komut.Parameters.AddWithValue("@P9", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtVergi.Text = "";
            TxtAlici.Text = "";
            TxtTEden.Text = "";
            TxtTAlan.Text = "";
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler frmFaturaUrunler = new FrmFaturaUrunler();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                frmFaturaUrunler.ID = dr["FATURABILGIID"].ToString();
            }
            frmFaturaUrunler.Show();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT URUNAD, SATISFIYAT FROM TBL_URUNLER WHERE ID = @P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
