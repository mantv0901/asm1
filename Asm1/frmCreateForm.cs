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
using System.Text.RegularExpressions;

namespace Asm1
{
    public partial class frmCreateForm : Form
    {
        public frmCreateForm()
        {
            InitializeComponent();
        }

        public IMemberRepository MemberRepository { get; set; }
        public Member member { get; set; }
        private void btSave_Click(object sender, EventArgs e)
        {
                var member = new Member
                {
                    Email = tbEmail.Text,
                    Password = tbPassword.Text,
                    City = tbCity.Text,
                    CompanyName = tbCompanyName.Text,
                    Country = tbCountry.Text,
                };
                bool check = MemberRepository.CreateMember(member);
            if (check = true)
            {
                MessageBox.Show("Create Succes!");
            }
            else
            {
                MessageBox.Show("Create Fail!");
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            var member = new Member
            {
                MemberId = int.Parse(tbMemberID.Text),
                Email = tbEmail.Text,
                Password = tbPassword.Text,
                City = tbCity.Text,
                CompanyName = tbCompanyName.Text,
                Country = tbCountry.Text,
            };
            
            bool check = MemberRepository.UpdateMember(member);
            if (check = true)
            {
                MessageBox.Show("Update Succes!");
            }
             else
            {
                MessageBox.Show("Update Fail!");
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {

            int memberID = int.Parse(tbMemberID.Text);

            bool check = MemberRepository.DeleteMember(memberID);
            if (check = true)
            {
                MessageBox.Show("Delete Succes!");
            }
            else
            {
                MessageBox.Show("Delete Fail!");
            }
        }
    }
}
