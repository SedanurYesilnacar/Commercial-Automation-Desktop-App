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
    public partial class FrmFaturaUrunler : Form
    {
        public FrmFaturaUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        public string ID;

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FATURADETAY WHERE FATURAID = '"+ ID +"' ", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmFaturaUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme frmFaturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                frmFaturaUrunDuzenleme.UrunID = dr["FATURAURUNID"].ToString();
            }
            frmFaturaUrunDuzenleme.Show();
        }
    }
}
