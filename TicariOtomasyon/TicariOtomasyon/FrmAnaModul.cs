using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class FrmAnaModul : Form
    {
        FrmUrunler frmUrunler;
        FrmMusteriler frmMusteriler;
        FrmFirmalar frmFirmalar;
        FrmPersonel frmPersonel;
        FrmRehber frmRehber;
        FrmGiderler frmGiderler;
        FrmBankalar frmBankalar;
        FrmFaturalar frmFaturalar;
        FrmNotlar frmNotlar;
        FrmHareketler frmHareketler;
        FrmRaporlar frmRaporlar;
        FrmStoklar frmStoklar;
        FrmAyarlar frmAyarlar;
        FrmKasa frmKasa;
        FrmAnasayfa frmAnasayfa;

        public string kullanici;

        public FrmAnaModul()
        {
            InitializeComponent();
        }

        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmUrunler == null || frmUrunler.IsDisposed)
            {
                frmUrunler = new FrmUrunler();
                frmUrunler.MdiParent = this;
                frmUrunler.Show();
            }
        }

        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmMusteriler == null || frmMusteriler.IsDisposed)
            {
                frmMusteriler = new FrmMusteriler();
                frmMusteriler.MdiParent = this;
                frmMusteriler.Show();
            }
        }

        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmFirmalar == null || frmFirmalar.IsDisposed)
            {
                frmFirmalar = new FrmFirmalar();
                frmFirmalar.MdiParent = this;
                frmFirmalar.Show();
            }
        }

        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmPersonel == null || frmPersonel.IsDisposed)
            {
                frmPersonel = new FrmPersonel();
                frmPersonel.MdiParent = this;
                frmPersonel.Show();
            }
        }

        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmRehber == null || frmRehber.IsDisposed)
            {
                frmRehber = new FrmRehber();
                frmRehber.MdiParent = this;
                frmRehber.Show();
            }
        }

        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmGiderler == null || frmGiderler.IsDisposed)
            {
                frmGiderler = new FrmGiderler();
                frmGiderler.MdiParent = this;
                frmGiderler.Show();
            }
        }

        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmBankalar == null || frmBankalar.IsDisposed)
            {
                frmBankalar = new FrmBankalar();
                frmBankalar.MdiParent = this;
                frmBankalar.Show();
            }
        }

        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmFaturalar == null || frmFaturalar.IsDisposed)
            {
                frmFaturalar = new FrmFaturalar();
                frmFaturalar.MdiParent = this;
                frmFaturalar.Show();
            }
        }

        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmNotlar == null || frmNotlar.IsDisposed)
            {
                frmNotlar = new FrmNotlar();
                frmNotlar.MdiParent = this;
                frmNotlar.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmHareketler == null || frmHareketler.IsDisposed)
            {
                frmHareketler = new FrmHareketler();
                frmHareketler.MdiParent = this;
                frmHareketler.Show();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmRaporlar == null || frmRaporlar.IsDisposed)
            {
                frmRaporlar = new FrmRaporlar();
                frmRaporlar.MdiParent = this;
                frmRaporlar.Show();
            }
        }

        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmStoklar == null || frmStoklar.IsDisposed)
            {
                frmStoklar = new FrmStoklar();
                frmStoklar.MdiParent = this;
                frmStoklar.Show();
            }
        }

        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmAyarlar == null || frmAyarlar.IsDisposed)
            {
                frmAyarlar = new FrmAyarlar();
                frmAyarlar.Show();
            }
        }

        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmKasa == null || frmKasa.IsDisposed)
            {
                frmKasa = new FrmKasa();
                frmKasa.ad = kullanici;
                frmKasa.MdiParent = this;
                frmKasa.Show();
            }
        }
        private void BtnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmAnasayfa == null || frmAnasayfa.IsDisposed)
            {
                frmAnasayfa = new FrmAnasayfa();
                frmAnasayfa.MdiParent = this;
                frmAnasayfa.Show();
            }
        }

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            if (frmAnasayfa == null || frmAnasayfa.IsDisposed)
            {
                frmAnasayfa = new FrmAnasayfa();
                frmAnasayfa.MdiParent = this;
                frmAnasayfa.Show();
            }
        }

    }
}
