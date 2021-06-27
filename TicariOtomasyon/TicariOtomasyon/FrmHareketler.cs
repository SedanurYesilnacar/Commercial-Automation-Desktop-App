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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmaHareketListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE FIRMA_HAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void musteriHareketListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE MUSTERI_HAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            firmaHareketListele();
            musteriHareketListele();
        }
    }
}
