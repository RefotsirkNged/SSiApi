using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Entities
{
  public class Location
  {
    public int LocationId
    {
      get; set;
    }

    //which row of shelves
    public int RowId
    {
      get; set;
    }

    public int ShelfId
    {
      get; set;
    }

    //which part of the given shelf is this location
    public int ShelfPartId
    {
      get; set;
    }

    //which shelf on the given row
    public int StockItemId
    {
      get; set;
    }
  }
}