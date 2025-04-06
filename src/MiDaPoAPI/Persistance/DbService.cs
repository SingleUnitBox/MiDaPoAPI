using Dapper;
using MiDaPoAPI.Models;
using Npgsql;

namespace MiDaPoAPI.Persistance;

public class DbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task<int> CreatePost(Post post)
    {
        var query = "INSERT INTO Posts (Title, Body, AuthorId) VALUES (@Title, @Body, @AuthorId) RETURNING Id";
        using var connection = GetConnection();
        return await connection.ExecuteScalarAsync<int>(query, post);
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        var query = "SELECT * FROM \"socialmedia.Posts\"";
        using var connection = GetConnection();
        return await connection.QueryAsync<Post>(query);
    }

    public async Task<int> FollowUser(Follow follow)
    {
        var query = "INSERT INTO Follows (FollowerId, FolloweeId) VALUES (@FollowerId, @FolloweeId)";
        using var connection = GetConnection();
        return await connection.ExecuteAsync(query, follow);
    }

    public async Task<int> LikePost(Like like)
    {
        var query = "INSERT INTO Likes (UserId, PostId) VALUES (@UserId, @PostId)";
        using var connection = GetConnection();
        return await connection.ExecuteAsync(query, like);
    }
}
