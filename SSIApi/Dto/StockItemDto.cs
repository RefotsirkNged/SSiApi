using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Dto
{
  public class StockItemDto
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