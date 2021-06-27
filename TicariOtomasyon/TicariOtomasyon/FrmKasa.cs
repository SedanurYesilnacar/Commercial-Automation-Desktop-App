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
using DevExpress.Charts;

namespace TicariOtomasyon
{
    public partial class FrmKasa : Form
    {
        public string ad;

        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void musteriHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE MUSTERI_HAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE FIRMA_HAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void giderHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void toplamTutarHesapla()
        {
            SqlCommand komut = new SqlCommand("SELECT SUM(TUTAR) FROM TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblKasaToplam.Text = dr[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
        }

        void faturaHesapla()
        {
            //Son ayın faturaları
            SqlCommand komut = new SqlCommand("SELECT (ELEKTRIK + SU + DOGALGAZ + EKSTRA) FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblOdemeler.Text = dr[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
        }

        void personelMaasHesapla()
        {
            //Son ayın personel maaşları
            SqlCommand komut = new SqlCommand("SELECT MAASLAR FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblPersonelMaas.Text = dr[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
        }

        void toplamMusteriSayisi()
        {
            //Toplam müşteri sayısı
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblMusteriSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void firmaSayisi()
        {
            //Toplam firma sayısı
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblFirmaSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void personelSayisi()
        {
            //Toplam personel sayısı
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblPersonelSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void firmaSehirSayisi()
        {
            //Toplam firma şehir sayısı
            SqlCommand komut = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblFSehirSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void musteriSehirSayisi()
        {
            //Toplam müşteri şehir sayısı
            SqlCommand komut = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblMSehirSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void stokSayisi()
        {
            //Toplam ürün sayısı
            SqlCommand komut = new SqlCommand("SELECT SUM(ADET) FROM TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblStokSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void chart1ElektrikLoad()
        {
            //Son 4 ay elektrik faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart1SuLoad()
        {
            //Son 4 ay su faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart1DogalgazLoad()
        {
            //Son 4 ay doğalgaz faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart1InternetLoad()
        {
            //Son 4 ay internet faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart1EkstraLoad()
        {
            //Son 4 ay ekstra listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart2ElektrikLoad()
        {
            //Son 4 ay elektrik faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart2SuLoad()
        {
            //Son 4 ay su faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart2DogalgazLoad()
        {
            //Son 4 ay doğalgaz faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart2InternetLoad()
        {
            //Son 4 ay internet faturası listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        void chart2EkstraLoad()
        {
            //Son 4 ay ekstra listeleme
            SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            bgl.baglanti().Close();
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;
            musteriHareket();
            firmaHareket();
            giderHareket();
            toplamTutarHesapla();
            faturaHesapla();
            personelMaasHesapla();
            toplamMusteriSayisi();
            firmaSayisi();
            personelSayisi();
            firmaSehirSayisi();
            musteriSehirSayisi();
            stokSayisi();
        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac > 0 && sayac <= 5)
            {
                groupControl10.Text = "Elektrik";
                chart1ElektrikLoad();
            }
            if(sayac > 5 && sayac <= 10)
            {
                groupControl10.Text = "Su";
                chartControl1.Series["AYLAR"].Points.Clear();
                chart1SuLoad();
            }
            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["AYLAR"].Points.Clear();
                chart1DogalgazLoad();
            }
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "İnternet";
                chartControl1.Series["AYLAR"].Points.Clear();
                chart1InternetLoad();
            }
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl1.Series["AYLAR"].Points.Clear();
                chart1EkstraLoad();
            }
            if(sayac == 26)
            {
                sayac = 0;
            }
        }

        int sayac2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl11.Text = "Elektrik";
                chart2ElektrikLoad();
            }
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["AYLAR"].Points.Clear();
                chart2SuLoad();
            }
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["AYLAR"].Points.Clear();
                chart2DogalgazLoad();
            }
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl2.Series["AYLAR"].Points.Clear();
                chart2InternetLoad();
            }
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl2.Series["AYLAR"].Points.Clear();
                chart2EkstraLoad();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
