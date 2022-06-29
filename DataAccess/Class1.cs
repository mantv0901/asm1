using System;
using DataAccess.DAO;
using BusinessLayer.Models;

namespace DataAccess
{
    public class Class1
    {
        static void Main()
        {
            MemberDAO dao = new MemberDAO();
            Member m  = new Member();
            m.CompanyName = "1";
            m.City = "TPHCM";
            m.Email = "qhuy1911@gmail.com";
            m.Country = "TPHCM";
            m.Password = "123123";

            bool rs = dao.Create(m);
            Console.WriteLine(rs);
        }
    }
}
