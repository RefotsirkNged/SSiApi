using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Interfaces
{
  public interface ILocationService
  {
    IEnumerable<Location> GetAll();

    Location GetById(int id);

    Location Create(Location location);

    void Update(Location location);

    void Delete(int id);
  }
}