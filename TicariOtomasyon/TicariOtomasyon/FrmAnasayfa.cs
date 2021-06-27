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
using System.Xml;

namespace TicariOtomasyon
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stokListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD, SUM(ADET) AS 'ADET' FROM TBL_URUNLER GROUP BY URUNAD HAVING SUM(ADET) <= 15 ORDER BY SUM(ADET) ASC", bgl.baglanti());
            da.Fill(dt);
            GridControlStoklar.DataSource = dt;
        }

        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 TARIH, SAAT, BASLIK FROM TBL_NOTLAR ORDER BY ID DESC", bgl.baglanti());
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;
        }

        void firmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE FIRMA_HAREKET2", bgl.baglanti());
            da.Fill(dt);
            GridControlHareket.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT AD, TELEFON1 FROM TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            GridControlFihrist.DataSource = dt;
        }

        void webBrowser()
        {
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/kurlar_tr.html");
        }

        void haberler()
        {
            XmlTextReader xmlTextReader = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while(xmlTextReader.Read())
            {
                if(xmlTextReader.Name == "title")
                {
                    listBox1.Items.Add(xmlTextReader.ReadString());
                }
            }
        }

        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            stokListele();
            ajanda();
            firmaHareket();
            fihrist();
            webBrowser();
            haberler();
        }
    }
}
