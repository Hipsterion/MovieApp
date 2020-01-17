using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MoviesApp.Domain;
using System.Data;
namespace MoviesApp.DAL
{
    public class MovieSqlRepository : ICrudRepository<Movie, int>
    {
        private readonly string _connectionString = "Server=DESKTOP-GEHGFPC\\SQLEXPRESS;Database=MovieDatabase;Trusted_Connection = True;";

        public IEnumerable<Movie> GetAll()
        {
            List<Movie> movies = new List<Movie>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Movies";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    movies.Add(new Movie()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"])
                    });
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
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                con.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    movie = new Movie()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"])
                    };
                }
            }
            return movie;            
        }

        public Movie Insert(Movie obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Movies (Title, ReleaseDate) VALUES (@Title, @ReleaseDate)" +
                    " SELECT SCOPE_IDENTITY()";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("@Title", obj.Title);
                cmd.Parameters.AddWithValue("@ReleaseDate", obj.ReleaseDate);
                con.Open();
                obj.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return obj;
        }

        public Movie Update(Movie obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Movies SET Title = @Title, ReleaseDate = @ReleaseDate WHERE Id = @Id";
                using SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("@Title", obj.Title);
                cmd.Parameters.AddWithValue("@ReleaseDate", obj.ReleaseDate);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return obj;
        }

        public void Delete(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            string query = "DELETE FROM Movies WHERE Id = @Id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
