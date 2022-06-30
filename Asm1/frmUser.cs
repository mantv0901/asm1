using BusinessLayer.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asm1
{
    public partial class frmUser : Form
    {
        public Member CurrentMember{ get; set; }
        public IMemberRepository MemberRepository { get; set; }

        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            CurrentMember = MemberRepository.GetMemberById(CurrentMember.MemberId);
            txtEmail.Text = CurrentMember.Email;
            txtMemberID.Text = CurrentMember.MemberId.ToString();
            txtCity.Text = CurrentMember.City;
            txtCountry.Text = CurrentMember.Country;
            txtPassword.Text = CurrentMember.Password;
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                btnShowPass.Text = "Hide";
            }
            else btnShowPass.Text = "Show";
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form form = new frmLogin();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }
    }
}
