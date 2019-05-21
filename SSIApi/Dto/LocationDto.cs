using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Dto
{
  public class LocationDto
  {
    public int LocationId
    {
      get; set;
    }

    public int RowId
    {
      get; set;
    }

    public int ShelfId
    {
      get; set;
    }

    public int ShelfPartId
    {
      get; set;
    }

    public int StockItemId
    {
      get; set;
    }
  }
}