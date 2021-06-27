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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_NOTLAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtBaslik.Text = "";
            RchDetay.Text = "";
            TxtOlusturan.Text = "";
            TxtHitap.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_NOTLAR (TARIH, SAAT, BASLIK, DETAY, OLUSTURAN, HITAP) VALUES (@P1, @P2, @P3, @P4, @P5, @P6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", MskTarih.Text);
            komut.Parameters.AddWithValue("@P2", MskSaat.Text);
            komut.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", RchDetay.Text);
            komut.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", TxtHitap.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtBaslik.Text = dr["BASLIK"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
                TxtOlusturan.Text = dr["OLUSTURAN"].ToString();
                TxtHitap.Text = dr["HITAP"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_NOTLAR WHERE ID = @P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_NOTLAR SET TARIH = @P1, SAAT = @P2, BASLIK = @P3, DETAY = @P4, OLUSTURAN = @P5, HITAP = @P6 WHERE ID = @P7", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", MskTarih.Text);
            komut.Parameters.AddWithValue("@P2", MskSaat.Text);
            komut.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", RchDetay.Text);
            komut.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", TxtHitap.Text);
            komut.Parameters.AddWithValue("@P7", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay frmNotDetay = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                frmNotDetay.metin = dr["DETAY"].ToString();
            }
            frmNotDetay.Show();
        }
    }
}
