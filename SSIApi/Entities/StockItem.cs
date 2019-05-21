using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Entities
{
  public class StockItem
  {
    public int StockItemId
    {
      get; set;
    }

    public int? ArticleId
    {
      get; set;
    }

    public int Stock
    {
      get; set;
    }

    public int? LocationId
    {
      get; set;
    }
  }
}