using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSSI.Models
{
  public class DataViewModel
  {
    public List<User> Users
    {
      get; set;
    }

    public List<Article> Articles
    {
      get; set;
    }

    public List<StockItem> StockItems
    {
      get; set;
    }

    public List<Location> Locations
    {
      get; set;
    }

    public Article newArticle
    {
      get; set;
    }

    public StockItem newStockItem
    {
      get; set;
    }

    public Location newLocation
    {
      get; set;
    }

    public User newUser
    {
      get; set;
    }
  }
}