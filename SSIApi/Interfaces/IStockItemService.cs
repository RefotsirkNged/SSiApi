using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Interfaces
{
  public interface IStockItemService
  {
    IEnumerable<StockItem> GetAll();

    StockItem GetById(int id);

    StockItem Create(StockItem item);

    void Update(StockItem item);

    void Delete(int id);
  }
}