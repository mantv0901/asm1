using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Repository;
using BusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Asm1
{
    public partial class frmLogin : Form
    {
        String Email, Password;
        IMemberRepository memberRepository = new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            Email = config.GetSection("AdminAccount")["Email"];
            Password = config.GetSection("AdminAccount")["Password"];
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String email = txtEmail.Text;
            String password = txtPassword.Text;
            if (email == Email && password == Password)
            {
                Form form = new Task4();
                this.Hide();
                form.ShowDialog();
                this.Close();
            }
            else
            {
                Member mem = memberRepository.CheckLogin(email, password);
                if (mem != null)
                {
                    Form form = new frmUser
                    {
                        CurrentMember = mem,
                        MemberRepository = memberRepository
                    };
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
                else
                {
                    lblMessage.Text = "Wrong Email or Password";
                }
            }
        }
    }
}
