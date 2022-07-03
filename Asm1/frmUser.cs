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

        private void load()
        {
            CurrentMember = MemberRepository.GetMemberById(CurrentMember.MemberId);
            txtEmail.Text = CurrentMember.Email;
            txtMemberID.Text = CurrentMember.MemberId.ToString();
            txtCity.Text = CurrentMember.City;
            txtCountry.Text = CurrentMember.Country;
            txtPassword.Text = CurrentMember.Password;
            txtCompany.Text = CurrentMember.CompanyName;
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            load();   
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

        private void btnChange_Click(object sender, EventArgs e)
        {
            CurrentMember.Email = txtEmail.Text;
            CurrentMember.City= txtCity.Text ;
            CurrentMember.Country =txtCountry.Text ;
            CurrentMember.Password=  txtPassword.Text;
            CurrentMember.CompanyName= txtCompany.Text ;
            MemberRepository.UpdateMember(CurrentMember);
            load();
            MessageBox.Show("Update Success");
        }
    }
}
