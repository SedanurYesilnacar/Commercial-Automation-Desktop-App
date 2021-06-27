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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtKullaniciAdi.Text = "";
            TxtSifre.Text = "";
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            temizle();
            listele();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(simpleButton3.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_ADMIN VALUES (@P1, @P2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni admin sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if(simpleButton3.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("UPDATE TBL_ADMIN SET Sifre = @P2 WHERE KullaniciAd = @P1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@P1", TxtKullaniciAdi.Text);
                komut1.Parameters.AddWithValue("@P2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtKullaniciAdi.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void TxtKullaniciAdi_EditValueChanged(object sender, EventArgs e)
        {
            if(TxtKullaniciAdi.Text != "")
            {
                simpleButton3.BackColor = Color.Wheat;
                simpleButton3.Text = "Güncelle";
            } else
            {
                simpleButton3.Text = "Kaydet";
            }
        }
    }
}
