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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD, SUM(ADET) AS 'SAYI' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void chartUrunData()
        {
            SqlCommand komut = new SqlCommand("SELECT URUNAD, SUM(ADET) AS 'SAYI' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString())); 
            }
            bgl.baglanti().Close();
        }

        void chartFirmaData()
        {
            SqlCommand komut = new SqlCommand("SELECT IL, COUNT(*) FROM TBL_FIRMALAR GROUP BY IL", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            listele();
            chartUrunData();
            chartFirmaData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay frmStokDetay = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr != null)
            {
                frmStokDetay.ad = dr["URUNAD"].ToString();
            }
            frmStokDetay.Show();
        }
    }
}
