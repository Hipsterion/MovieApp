using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using MoviesApp.Domain;
using Dapper;
using Microsoft.Extensions.Options;

namespace MoviesApp.DAL
{
    public class MovieDapSqlRepository : IMovieRepository
    {
        public AppConfigData ConfigData { get; }
        private readonly string _connectionString;

        public MovieDapSqlRepository(IOptions<AppConfigData> configData)
        {
            ConfigData = configData.Value;
            _connectionString = ConfigData.ConnectionString;
        }
        public IEnumerable<Movie> GetAll()
        {
            IEnumerable<Movie> movies = new List<Movie>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                movies = con.Query<Movie>("SELECT * FROM Movies");
            }
            return movies;
        }

        public Movie GetById(int id)
        {
            Movie movie = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                movie = con.QueryFirst<Movie>("Select * FROM Movies WHERE Id = @Id", new { Id = id });
            }
            return movie;
        }

        public Movie Insert(Movie obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("INSERT INTO Movies (Title, ReleaseDate) VALUES (@Title, @ReleaseDate)" +
                    " SELECT SCOPE_IDENTITY()", new { Title = obj.Title, ReleaseDate = obj.ReleaseDate });
            }
            return obj;
        }

        public Movie Update(Movie obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("UPDATE Movies SET Title = @Title, ReleaseDate = @ReleaseDate WHERE Id = @Id", new { Id = obj.Id, Title = obj.Title, ReleaseDate = obj.ReleaseDate });
            }
            return obj;
        }

        public void Delete(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Execute("DELETE FROM Votes WHERE MovieId = @Id;" +
                        "DELETE FROM Movies WHERE Id = @Id", new { Id = id});
        }

        public int GetMovieVotesCount(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            return con.QueryFirst<int>("SELECT COUNT(*) FROM Votes WHERE MovieId = @Id", new { Id = id });
        }

        public double GetMovieRating(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            return con.QueryFirst<double>("SELECT ISNULL(AVG(ALL Score), 0) FROM Votes WHERE MovieId = @Id", new { Id = id });
        }

        public IEnumerable<Vote> GetMovieVotes(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            return con.Query<Vote>("SELECT * FROM Votes WHERE MovieId = @Id", new { Id = id });
        }
    }
}
