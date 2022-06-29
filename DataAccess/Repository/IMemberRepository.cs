using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        Member GetMemberById(int id);
        Member CheckLogin(String Email, String Password);
        bool CreateMember(Member member);
        bool UpdateMember(Member member);
        bool DeleteMember(int id);
        List<string> GetCities();
        List<string> GetCountries();
        List<Member> GetMembersByEmail(String Email);
        List<Member> GetAllMember();
        List<Member> GetMembersByCity(String City);
        List<Member> GetMembersByCountry(String Country);
    }
}
