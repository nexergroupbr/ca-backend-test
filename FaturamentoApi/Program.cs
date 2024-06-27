using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FaturamentoApi.Models;
using System.Text.Json.Serialization;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/v1", async (AppDbContext db) => await db.Produtos.ToListAsync());

app.MapGet("/api/v1/produtos/{id:int}", async (int id, AppDbContext db) =>
{
    return await db.Produtos.FindAsync(id)
        is Produto produto
            ? Results.Ok(produto) : Results.NotFound();
});

app.MapPost("/api/v1/produtos", async (Produtos produto, AppDbContext db) =>
{
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();

    return Results.Created($"/api/v1/produtos/{produto.ProdutoId}", produto);
});
app.MapPut("/api/v1/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
{
    if (produto.ProdutoId != id)
    {
        return Results.BadRequest();
    }

    var produtoDB = await db.Produtos.FindAsync(id);

    if (produtoDB is null) return Results.NotFound();

    produtoDB.Nome = produto.Nome;

    await db.SaveChangesAsync();
    return Results.Ok(produtoDB);
}); ;

app.MapDelete("/api/v1/produtos/{id:int}", async (int id, AppDbContext db) =>
{
    var produtos = await db.Produtos.FindAsync(id);

    if (produtos is null)
    {
        return Results.NotFound();
    }
    db.Produtos.Remove(produtos);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();
