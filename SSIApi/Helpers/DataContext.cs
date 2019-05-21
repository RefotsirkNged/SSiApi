using Microsoft.EntityFrameworkCore;
using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Helpers
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //auto incrementing ids
      modelBuilder.Entity<User>()
        .Property(p => p.UserId)
        .ValueGeneratedOnAdd();
      modelBuilder.Entity<Article>()
        .Property(p => p.ArticleId)
        .ValueGeneratedOnAdd();
      modelBuilder.Entity<StockItem>()
        .Property(p => p.StockItemId)
        .ValueGeneratedOnAdd();
      modelBuilder.Entity<Location>()
        .Property(p => p.LocationId)
        .ValueGeneratedOnAdd();
    }

    public DbSet<User> Users
    {
      get; set;
    }

    public DbSet<Article> Articles
    {
      get; set;
    }

    public DbSet<Location> Locations
    {
      get; set;
    }

    public DbSet<StockItem> StockItems
    {
      get; set;
    }
  }
}