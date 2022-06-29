using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DataAccess.DAO;
namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member CheckLogin(string Email, string Password)
        {
            return MemberDAO.Instance.CheckLogin(Email, Password);
        }

        public bool CreateMember(Member member)
        {
            return MemberDAO.Instance.Create(member);
        }

        public bool DeleteMember(int id)
        {
            return MemberDAO.Instance.Delete(id);
        }

        public List<Member> GetAllMember()
        {
            return MemberDAO.Instance.GetAllMember();
        }

        public List<string> GetCities()
        {
            return MemberDAO.Instance.GetCities();
        }

        public List<string> GetCountries()
        {
            return MemberDAO.Instance.GetCountries();
        }

        public Member GetMemberById(int id)
        {
            return MemberDAO.Instance.GetMemberById(id);
        }

        public List<Member> GetMembersByCity(string City)
        {
            return MemberDAO.Instance.GetMemberByCityOrCountry(City, true);
        }

        public List<Member> GetMembersByCountry(string Country)
        {
            return MemberDAO.Instance.GetMemberByCityOrCountry(Country, false);
        }

        public List<Member> GetMembersByEmail(string Email)
        {
            return MemberDAO.Instance.GetMemberByEmail(Email);
        }

        public bool UpdateMember(Member member)
        {
            return MemberDAO.Instance.Update(member);
        }
    }
}
