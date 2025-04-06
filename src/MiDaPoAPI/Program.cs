using Microsoft.EntityFrameworkCore;
using MiDaPoAPI.Models;
using MiDaPoAPI.Persistance;

namespace MiDaPoAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<MiDaPoDbContext>(options
            => options.UseNpgsql("Host=localhost;Username=postgres;Password=czcz;Database=socialmedia"));
        builder.Services.AddScoped<DbService>();
        
        var app = builder.Build();

        app.MapPost("/posts", async (Post post, DbService dbService) =>
        {
            var postId = await dbService.CreatePost(post);
            return Results.Created($"/posts/{postId}", post);
        });

        app.MapGet("/posts", async (DbService DbService) =>
        {
            var posts = await DbService.GetPosts();
            return Results.Ok(posts);
        });

        app.MapPost("/follow", async (Follow follow, DbService dbService) =>
        {
            await dbService.FollowUser(follow);
            return Results.Ok();
        });

        app.MapPost("/like", async (Like like, DbService dbService) =>
        {
            await dbService.LikePost(like);
            return Results.Ok();
        });
        
        app.Run();
    }
}