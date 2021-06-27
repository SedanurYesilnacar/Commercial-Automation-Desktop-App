using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace TicariOtomasyon
{
    public partial class FrmMail : Form
    {
        public string mail;
        
        public FrmMail()
        {
            InitializeComponent();
        }

        private void FrmMail_Load(object sender, EventArgs e)
        {
            textEdit1.Text = mail;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new NetworkCredential("mymail","mypassword");
            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(textEdit1.Text);
            mesajim.From = new MailAddress("mymail");
            mesajim.Subject = textEdit2.Text;
            mesajim.Body = richTextBox1.Text;
            istemci.Send(mesajim);
        }
    }
}
