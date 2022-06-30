using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private  static MemberDAO instance;
        private readonly static object lockInstance = new object();
        public static MemberDAO Instance { get
            {
                lock (lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        
        public  String GetConnectionString()
        {
            String connectionString;
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true).Build();
            return config.GetConnectionString("DefaultConnection");

        }
        public  bool Create(Member member)
        {
            String sql = "INSERT INTO Member(Email,Password,CompanyName,City,Country)" +
                " VALUES(@Email,@Password,@CompanyName,@City,@Country)";
            bool check = false;
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Password", member.Password);
                command.Parameters.AddWithValue("@CompanyName", member.CompanyName);
                command.Parameters.AddWithValue("@City",member.City);
                command.Parameters.AddWithValue("@Country", member.Country);
                try
                {
                    connection.Open();
                    int a = command.ExecuteNonQuery();
                    if (a > 0) check = true;
                    connection.Close();
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return check;
        }
        public Member GetMemberById(int id)
        {
            Member member = null;
            String sql = "Select MemberID,Email,Password,CompanyName,City,Country From Member " +
                " Where MemberID=@ID";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        member = new Member()
                        {
                            MemberId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                            CompanyName = reader.GetString(3),
                            City = reader.GetString(4),
                            Country = reader.GetString(5)
                        };
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return member;
        }
        public Member CheckLogin(string Email,string Password)
        {
            Member member = null;
            String sql = "Select MemberID,Email,Password,CompanyName,City,Country From Member " +
                " Where Email=@Email and Password=@Password and [Status] = 1";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Password", Password);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        member = new Member()
                        {
                            MemberId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                            CompanyName = reader.GetString(3),
                            City = reader.GetString(4),
                            Country = reader.GetString(5)
                        };
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return member;
        }
        public List<Member> GetMemberByEmail(String Email)
        {
            List<Member> members = new List<Member>();
            String sql = "Select MemberID,Email,Password,CompanyName,City,Country From Member " +
                " Where Email=@Email";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", Email);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Member member = new Member()
                        {
                            MemberId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                            CompanyName = reader.GetString(3),
                            City = reader.GetString(4),
                            Country = reader.GetString(5)
                        };
                        members.Add(member);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return members;
        }
        public List<Member> GetMemberByCityOrCountry(String search, bool isCity)
        {
            List<Member> members = new List<Member>();
            String sql = "Select MemberID,Email,Password,CompanyName,City,Country From Member " +
                " Where Country=@Country and [Status] = 1";
            if (isCity)
                sql = "Select MemberID,Email,Password,CompanyName,City,Country From Member " +
                " Where City=@City and [Status] = 1";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                if (isCity) command.Parameters.AddWithValue("@City", search);
                else command.Parameters.AddWithValue("@Country", search);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Member member = new Member()
                        {
                            MemberId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                            CompanyName = reader.GetString(3),
                            City = reader.GetString(4),
                            Country = reader.GetString(5)
                        };
                        members.Add(member);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return members;
        }
        public List<string> GetCities()
        {
            List<string> cities = new List<string>();
            String sql = "Select City from Member " +
                " group by City";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        cities.Add(reader.GetString(0));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return cities;
        }
        public List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            String sql = "Select Country from Member " +
                " group by Country";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        countries.Add(reader.GetString(0));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return countries;
        }
        public bool Update(Member member)
        {
            String sql = "UPDATE Member SET Email=@Email,Password=@Password,CompanyName=@CompanyName,City=@City,Country=@Country" +
                " WHERE MemberID=@ID";
            bool check = false;
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@Password", member.Password);
                command.Parameters.AddWithValue("@CompanyName", member.CompanyName);
                command.Parameters.AddWithValue("@City", member.City);
                command.Parameters.AddWithValue("@Country", member.Country);
                command.Parameters.AddWithValue("@ID", member.MemberId);
                try
                {
                    connection.Open();
                    int a = command.ExecuteNonQuery();
                    if (a > 0) check = true;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return check;
        }
        public List<Member> GetAllMember()
        {
            List<Member> members = new List<Member>();
            String sql = "select * from Member";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Member member = new Member()
                        {
                            MemberId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(5),
                            CompanyName = reader.GetString(2),
                            City = reader.GetString(3),
                            Country = reader.GetString(4)
                        };
                        members.Add(member);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return members;
        }
        public bool Delete(int id)
        {
            String sql = "UPDATE Member SET [Status] =0" +
                " WHERE MemberID=@ID";
            bool check = false;
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                
                command.Parameters.AddWithValue("@ID", id);
                try
                {
                    connection.Open();
                    int a = command.ExecuteNonQuery();
                    if (a > 0) check = true;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return check;
        }
    }
}
