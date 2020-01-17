using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.Domain;
using System.Data;
using System.Data.SqlClient;

namespace MoviesApp.DAL
{
    public class MemberSqlRepository : ICrudRepository<Member, int>
    {
        private readonly string _connectionString = "Server=DESKTOP-GEHGFPC\\SQLEXPRESS;Database=MovieDatabase;Trusted_Connection = True;";

        public IEnumerable<Member> GetAll()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Members";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    members.Add(new Member()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        RealName = Convert.ToString(dr["RealName"]),
                        Email = Convert.ToString(dr["Email"])
                    });
                }
            }
            return members;
        }

        public Member GetById(int id)
        {
            Member member = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "Select * FROM Members WHERE Id=@Id";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                con.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    member = new Member()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        RealName = Convert.ToString(dr["RealName"]),
                        Email = Convert.ToString(dr["Email"])
                    };
                }
            }
            return member;
        }

        public Member Insert(Member obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Members (RealName, Email) VALUES (@RealName, @Email)" +
                    " SELECT SCOPE_IDENTITY()";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("@RealName", obj.RealName);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                con.Open();
                obj.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return obj;
        }

        public Member Update(Member obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Members SET RealName = @RealName, Email = @Email WHERE Id = @Id";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("@RealName", obj.RealName);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return obj;
        }

        public void Delete(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            string query = "DELETE FROM Members WHERE Id = @Id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
