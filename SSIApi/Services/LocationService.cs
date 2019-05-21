using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSIApi.Entities;
using SSIApi.Helpers;
using SSIApi.Interfaces;

namespace SSIApi.Services
{
  public class LocationService : ILocationService
  {
    private DataContext context;

    public LocationService(DataContext _context)
    {
      context = _context;
    }

    public Location Create(Location location)
    {
      location.LocationId = 0; //set to 0, so the auto incrementing column is used in postgres

      if (!context.StockItems.Any(x => x.StockItemId == location.StockItemId))
      {
        throw new ApplicationException("StockItem not found");
      }
      if (context.Locations.Any(x => x.StockItemId == location.StockItemId))
      {
        throw new ApplicationException("Stockitem already in existing location.");
      }
      context.Locations.Add(location);
      context.SaveChanges();

      //We need to update the corresponding stockItems, with the updated id assigned by the DB (Thus the double SaveChanges)
      var stockItem = context.StockItems.Find(location.StockItemId);
      stockItem.LocationId = location.LocationId;
      context.Update(stockItem);
      context.SaveChanges();

      return location;
    }

    public IEnumerable<Location> GetAll()
    {
      return context.Locations;
    }

    public Location GetById(int id)
    {
      return context.Locations.Find(id);
    }

    public void Update(Location locationParam)
    {
      var location = context.Locations.Find(locationParam.LocationId);
      if (location == null)
      {
        throw new ApplicationException("Location not found");
      }

      if (location.StockItemId != locationParam.StockItemId)
      {
        if (context.Locations.Any(x => x.StockItemId == locationParam.StockItemId))
        {
          throw new ApplicationException("StockItemId " + locationParam.StockItemId + "is already placed in a location.");
        }
      }

      if (location.RowId != locationParam.RowId || location.ShelfId != locationParam.ShelfId || location.ShelfPartId != locationParam.ShelfPartId)
      {
        if (context.Locations.Any(x => x.RowId == locationParam.RowId && x.ShelfId == locationParam.ShelfId && x.ShelfPartId == locationParam.ShelfPartId))
        {
          throw new ApplicationException("New Location parameters already exists as a location.");
        }
      }
      location.RowId = locationParam.RowId;
      location.ShelfId = locationParam.ShelfId;
      location.ShelfPartId = locationParam.ShelfPartId;
      location.StockItemId = locationParam.StockItemId;
      context.Locations.Update(location);
      context.SaveChanges();
    }

    public void Delete(int id)
    {
      var location = context.Locations.Find(id);
      if (location != null)
      {
        context.Locations.Remove(location);
        context.SaveChanges();
      }
    }
  }
}