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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE BANKABILGILERI",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtBankaAd.Text = "";
            CBEil.Text = "";
            CBEilce.Text = "";
            TxtSube.Text = "";
            TxtIBAN.Text = "";
            TxtHesapNo.Text = "";
            TxtYetkili.Text = "";
            MskTlf.Text = "";
            MskTarih.Text = "";
            TxtHesapTur.Text = "";
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

        void firmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID, AD FROM TBL_FIRMALAR",bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirListesi();
            firmaListesi();
        }

        private void CBEil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListesi();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_BANKALAR (BANKAADI, IL, ILCE, SUBE, IBAN, HESAPNO, YETKILI, TELEFON, TARIH, HESAPTURU, FIRMAID) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@P2", CBEil.Text);
            komut.Parameters.AddWithValue("@P3", CBEilce.Text);
            komut.Parameters.AddWithValue("@P4", TxtSube.Text);
            komut.Parameters.AddWithValue("@P5", TxtIBAN.Text);
            komut.Parameters.AddWithValue("@P6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", MskTlf.Text);
            komut.Parameters.AddWithValue("@P9", MskTarih.Text);
            komut.Parameters.AddWithValue("@P10", TxtHesapTur.Text);
            komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgileri kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtBankaAd.Text = dr["BANKAADI"].ToString();
                CBEil.Text = dr["IL"].ToString();
                CBEilce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtIBAN.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTlf.Text = dr["TELEFON"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                TxtHesapTur.Text = dr["HESAPTURU"].ToString();
                lookUpEdit1.Text = dr["AD"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_BANKALAR WHERE ID= @P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgisi silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_BANKALAR SET BANKAADI = @P1, IL = @P2, ILCE = @P3, SUBE = @P4, IBAN = @P5, HESAPNO = @P6, YETKILI = @P7, TELEFON = @P8, TARIH = @P9, HESAPTURU = @P10, FIRMAID = @P11 WHERE ID = @P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@P2", CBEil.Text);
            komut.Parameters.AddWithValue("@P3", CBEilce.Text);
            komut.Parameters.AddWithValue("@P4", TxtSube.Text);
            komut.Parameters.AddWithValue("@P5", TxtIBAN.Text);
            komut.Parameters.AddWithValue("@P6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", MskTlf.Text);
            komut.Parameters.AddWithValue("@P9", MskTarih.Text);
            komut.Parameters.AddWithValue("@P10", TxtHesapTur.Text);
            komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@P12", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
