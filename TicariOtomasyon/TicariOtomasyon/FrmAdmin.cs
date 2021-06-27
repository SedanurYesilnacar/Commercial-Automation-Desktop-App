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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_ADMIN WHERE KullaniciAd = @P1 and Sifre = @P2", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmAnaModul frmAnaModul = new FrmAnaModul();
                frmAnaModul.kullanici = TxtKullaniciAd.Text;
                frmAnaModul.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Hatalı kullanıcı adı ya da şifre","", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.Blue;

        }

        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.MintCream;
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
