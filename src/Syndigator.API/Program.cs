using Microsoft.EntityFrameworkCore;
using Syndigator.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddNpgsqlDbContext<ApiDbContext>("syndigatorDb");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

// TODO: Move endpoints to NameApi.cs and group
app.MapGet("/feeds", async (ApiDbContext db) =>
{
    return await db.Feeds.ToListAsync();
});

app.MapGet("/feeds/{id}", async (ApiDbContext db, int id) =>
{
    return await db.Feeds.FindAsync(id);
});

app.MapPost("/feeds", async (ApiDbContext db, Feed feed) =>
{
    db.Feeds.Add(feed);
    await db.SaveChangesAsync();
    return Results.Created($"/feeds/{feed.FeedId}", feed);
});

app.MapPut("/feeds/{id}", async (ApiDbContext db, int id, Feed feed) =>
{
    if (id != feed.FeedId)
    {
        return Results.BadRequest();
    }

    db.Entry(feed).State = EntityState.Modified;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/items", async (ApiDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapGet("/items/{id}", async (ApiDbContext db, int id) =>
{
    return await db.Items.FindAsync(id);
});

app.MapPost("/items", async (ApiDbContext db, Item item) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{item.ItemId}", item);
});

app.Run();