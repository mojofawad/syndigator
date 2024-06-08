using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddNpgsqlDbContext<SyndigatorContext>("postgres");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/feeds", async (SyndigatorContext db) =>
{
    return await db.Feeds.ToListAsync();
});

app.MapGet("/feeds/{id}", async (SyndigatorContext db, int id) =>
{
    return await db.Feeds.FindAsync(id);
});

app.MapPost("/feeds", async (SyndigatorContext db, Feed feed) =>
{
    db.Feeds.Add(feed);
    await db.SaveChangesAsync();
    return Results.Created($"/feeds/{feed.FeedId}", feed);
});

app.MapPut("/feeds/{id}", async (SyndigatorContext db, int id, Feed feed) =>
{
    if (id != feed.FeedId)
    {
        return Results.BadRequest();
    }

    db.Entry(feed).State = EntityState.Modified;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/items", async (SyndigatorContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapGet("/items/{id}", async (SyndigatorContext db, int id) =>
{
    return await db.Items.FindAsync(id);
});

app.MapPost("/items", async (SyndigatorContext db, Item item) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{item.ItemId}", item);
});

app.Run();

public class Feed
{
    public int FeedId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    
    public FeedType Type { get; }
    
    public List<Item> Items { get; set; }
}

public class Item
{
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Content { get; set; }
    public DateTime Published { get; set; }
    
    public Feed Feed { get; set; }
    public int FeedId { get; set; }
}

public enum FeedType
{
    Rss,
    Atom,
    Rdf,
    Unknown
}

public class SyndigatorContext : DbContext
{
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<Item> Items { get; set; }
    
    public SyndigatorContext(DbContextOptions<SyndigatorContext> options) : base(options)
    {
    }
}

