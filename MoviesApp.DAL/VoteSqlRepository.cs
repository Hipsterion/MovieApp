using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MoviesApp.Domain;

namespace MoviesApp.DAL
{
    public class VoteSqlRepository : ICrudRepository<Vote, (int, int)>
    {
        private readonly string _connectionString = "Server=DESKTOP-GEHGFPC\\SQLEXPRESS;Database=MovieDatabase;Trusted_Connection = True;";

        public IEnumerable<Vote> GetAll()
        {
            List<Vote> votes = new List<Vote>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Votes";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    votes.Add(new Vote()
                    {
                        Id = (Convert.ToInt32(dr["MemberId"]), Convert.ToInt32(dr["MovieId"])),
                        Score = Convert.ToInt32(dr["Score"])
                    });
                }
            }
            return votes;
        }

        public Vote GetById((int, int) key)
        {
            Vote vote = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "Select * FROM Votes WHERE MemberId = @MemberId AND MovieId = @MovieId";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@MemberId", SqlDbType.Int).Value = key.Item1;
                cmd.Parameters.Add("@MovieId", SqlDbType.Int).Value = key.Item2;
                con.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    vote = new Vote()
                    {
                        Id =(MemberId : Convert.ToInt32(dr["MemberId"]), MovieId : Convert.ToInt32(dr["MovieId"])),
                        Score = Convert.ToInt32(dr["Score"])
                    };
                }
            }
            return vote;
        }

        public Vote Insert(Vote obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Votes (Score, MemberId, MovieId) VALUES (@Score, @MemberId, @MovieId)" +
                    " SELECT SCOPE_IDENTITY()";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@MemberId", SqlDbType.Int).Value = obj.Id.Item1;
                cmd.Parameters.Add("@MovieId", SqlDbType.Int).Value = obj.Id.Item2;
                cmd.Parameters.Add("@Score", SqlDbType.Int).Value = obj.Score;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return obj;
        }

        public Vote Update(Vote obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Votes SET MemberId = @MemberId, MovieId = @MovieId, Score = @Score WHERE MemberId = @MemberId AND MovieId = @MovieId";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@MemberId", SqlDbType.Int).Value = obj.Id.Item1;
                cmd.Parameters.Add("@MovieId", SqlDbType.Int).Value = obj.Id.Item2;
                cmd.Parameters.Add("@Score", SqlDbType.Int).Value = obj.Score;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return obj;
        }

        public void Delete((int, int) key)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            string query = "DELETE FROM Votes WHERE MemberId = @MemberId AND MovieId = @MovieId";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@MemberId", SqlDbType.Int).Value = key.Item1;
            cmd.Parameters.Add("@MovieId", SqlDbType.Int).Value = key.Item2;
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
