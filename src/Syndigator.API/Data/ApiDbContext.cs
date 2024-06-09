using Microsoft.EntityFrameworkCore;

namespace Syndigator.API.Data;

// TODO: Add Migrations
public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<Item> Items { get; set; }
}