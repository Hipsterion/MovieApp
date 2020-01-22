using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Microsoft.Extensions.Options;
using MoviesApp.Domain;

namespace MoviesApp.DAL
{
    public class MemberDapSqlRepository : IMemberRepository
    {
        public AppConfigData ConfigData { get; }
        private readonly string _connectionString;

        public MemberDapSqlRepository(IOptions<AppConfigData> configData)
        {
            ConfigData = configData.Value;
            _connectionString = ConfigData.ConnectionString;
        }
        public IEnumerable<Member> GetAll()
        {
            IEnumerable<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                members = con.Query<Member>("SELECT * FROM Members");
            }
            return members;
        }

        public Member GetById(int id)
        {
            Member member = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                member = con.QueryFirst<Member>("SELECT * FROM Members WHERE Id = @Id", new { Id = id });
            }
            return member;
        }

        public Member Insert(Member obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("INSERT INTO Members (RealName, Email) VALUES (@RealName, @Email)" +
                    " SELECT SCOPE_IDENTITY()", new { RealName = obj.RealName, Email = obj.Email });
            }
            return obj;
        }

        public Member Update(Member obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("UPDATE Members SET RealName = @RealName, Email = @Email WHERE Id = @Id", new { Id = obj.Id, RealName = obj.RealName, Email = obj.Email});
            }
            return obj;
        }

        public void Delete(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Execute("DELETE FROM Votes WHERE MemberId = @Id;" +
                    "DELETE FROM Members WHERE Id = @Id", new { Id = id });
        }
    }
}
