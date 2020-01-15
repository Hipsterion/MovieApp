using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MoviesApp.Domain;
using System.Configuration;
using System.Data;
namespace MoviesApp.DAL
{
    public class SqlRepository : ICrudRepository<Movie, int>
    {
        private readonly string _connectionString = "Server=DESKTOP-KR810M7;Database=MovieDatabase;Trusted_Connection = True;";
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            List<Movie> movies = new List<Movie>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Movies";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        movies.Add(new Movie()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Popularity = Convert.ToDouble(dr["Popularity"]),
                            VoteCount = Convert.ToInt32(dr["VoteCount"]),
                            Title = Convert.ToString(dr["Title"]),
                            ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"])
                        });
                    }
                }
            }
            return movies;
        }

        public Movie GetById(int id)
        {
            Movie movie = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "Select * FROM Movies WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        movie = new Movie()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Popularity = Convert.ToDouble(dr["Popularity"]),
                            VoteCount = Convert.ToInt32(dr["VoteCount"]),
                            Title = Convert.ToString(dr["Title"]),
                            ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"])
                        };
                    }
                }
            }
            return movie;            
        }

        public Movie Insert(Movie obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Movies (Popularity, VoteCount, Title, ReleaseDate) VALUES (@Popularity, @VoteCount, @Title, @ReleaseDate)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@Popularity", SqlDbType.Float).Value = obj.Popularity;
                    cmd.Parameters.Add("@VoteCount", SqlDbType.Int).Value = obj.Popularity;
                    cmd.Parameters.AddWithValue("@Title", obj.Title);
                    cmd.Parameters.AddWithValue("@ReleaseDate", obj.ReleaseDate);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Movie Update(Movie obj)
        {
            throw new NotImplementedException();
        }
    }
}
