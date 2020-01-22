using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using MoviesApp.Domain;
using System.Linq;
using Microsoft.Extensions.Options;

namespace MoviesApp.DAL
{
    class VoteDapSqlRepository : IVoteRepository
    {
        public AppConfigData ConfigData { get; }
        private readonly string _connectionString;

        public VoteDapSqlRepository(IOptions<AppConfigData> configData)
        {
            ConfigData = configData.Value;
            _connectionString = ConfigData.ConnectionString;
        }

        public IEnumerable<Vote> GetAll()
        {
            IEnumerable<Vote> votes = new List<Vote>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                votes = con.Query<Vote>("SELECT * FROM Votes");
            }
            return votes;
        }

        public Vote GetById((int, int) key)
        {
            Vote vote = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                vote = con.QueryFirst<Vote>("SELECT * FROM Votes WHERE MemberId = @MemberId AND MovieId = @MovieId", new { MemberId = key.Item1, MovieId = key.Item2});
            }
            return vote;
        }

        public Vote Insert(Vote obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("INSERT INTO Votes (Score, MemberId, MovieId) VALUES (@Score, @MemberId, @MovieId)" +
                    " SELECT SCOPE_IDENTITY()", new { Score = obj.Score, MemberId = obj.Id.MemberId, MovieId = obj.Id.MovieId });
            }
            return obj;
        }

        public Vote Update(Vote obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("UPDATE Votes SET MemberId = @MemberId, MovieId = @MovieId, Score = @Score WHERE MemberId = @MemberId AND MovieId = @MovieId", new { Score = obj.Score, MemberId = obj.Id.MemberId, MovieId = obj.Id.MovieId });
            }
            return obj;
        }

        public void Delete((int, int) key)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Execute("DELETE FROM Votes WHERE MemberId = @MemberId AND MovieId = @MovieId", new { MemberId = key.Item1, MovieId = key.Item2 });
        }
    }
}
