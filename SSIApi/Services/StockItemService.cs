using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSIApi.Entities;
using SSIApi.Helpers;
using SSIApi.Interfaces;

namespace SSIApi.Services
{
  public class StockItemService : IStockItemService
  {
    private DataContext context;

    public StockItemService(DataContext _context)
    {
      context = _context;
    }

    public StockItem Create(StockItem item)
    {
      item.StockItemId = 0; //set to 0, so the auto incrementing column is used in postgres
      if (context.StockItems.Any(x => x.LocationId == item.LocationId))
      {
        throw new ApplicationException("Location is already used in different stockitem. Items cannot occupy the same space.");
      }
      item.LocationId = null;
      item.ArticleId = null;
      context.StockItems.Add(item);
      context.SaveChanges();
      return item;
    }

    public IEnumerable<StockItem> GetAll()
    {
      return context.StockItems;
    }

    public StockItem GetById(int id)
    {
      return context.StockItems.Find(id);
    }

    public void Update(StockItem item)
    {
      var stockItem = context.StockItems.Find(item.StockItemId);
      if (stockItem == null)
      {
        throw new ApplicationException("StockItem not found");
      }

      //Check if a new location is provided, check location is not occupied, check if location exists
      if (stockItem.LocationId != item.LocationId && item.LocationId != null)
      {
        if (context.StockItems.Any(x => x.LocationId == item.LocationId))
        {
          throw new ApplicationException("Location " + item.LocationId + "already contains stock.");
        }

        if (context.Locations.Any(x => x.LocationId == item.LocationId))
        {
          throw new ApplicationException("Location not found");
        }
      }

      if (stockItem.ArticleId != item.ArticleId && item.ArticleId != null)
      {
        if (!context.Articles.Any(x => x.ArticleId == item.ArticleId))
        {
          throw new ApplicationException("Article id not found.");
        }
      }

      //Check the stock for negative values
      if (stockItem.Stock != item.Stock)
      {
        if (item.Stock < 0)
        {
          throw new ApplicationException("Stock " + item.Stock + " cannot be negative.");
        }
      }

      if (item.ArticleId != null)
      {
        stockItem.ArticleId = item.ArticleId;
      }
      if (item.LocationId != null)
      {
        stockItem.LocationId = item.LocationId;
      }

      stockItem.Stock = item.Stock;

      context.StockItems.Update(stockItem);
      context.SaveChanges();
    }

    public void Delete(int id)
    {
      var stockItem = context.StockItems.Find(id);
      if (stockItem != null)
      {
        context.StockItems.Remove(stockItem);
        context.SaveChanges();
      }
    }
  }
}