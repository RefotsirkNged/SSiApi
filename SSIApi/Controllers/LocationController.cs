using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SSIApi.Dto;
using SSIApi.Entities;
using SSIApi.Helpers;
using SSIApi.Interfaces;

namespace SSIApi.Controllers
{
  [ResponseCache(NoStore = true, Duration = 0)]
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class LocationsController : ControllerBase
  {
    private IMapper mapper;
    private readonly AppSettings appSettings;
    private ILocationService locationService;

    public LocationsController(IMapper _mapper, IOptions<AppSettings> _appSettings, ILocationService _locationService)
    {
      mapper = _mapper;
      appSettings = _appSettings.Value;
      locationService = _locationService;
    }

    [HttpPost]
    public IActionResult Create([FromBody]LocationDto locationDto)
    {
      Location location = mapper.Map<Location>(locationDto);
      try
      {
        locationService.Create(location);
        return Ok(location);
      }
      catch (ApplicationException ex)
      {
        return BadRequest(new
        {
          message = ex.Message
        });
      }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var locations = locationService.GetAll();
      var locationDtos = mapper.Map<IList<LocationDto>>(locations);

      return Ok(locationDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var location = locationService.GetById(id);
      var locationDto = mapper.Map<StockItemDto>(location);
      return Ok(locationDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody]LocationDto locationDto)
    {
      var location = mapper.Map<Location>(locationDto);
      location.LocationId = id;

      try
      {
        locationService.Update(location);
        return Ok();
      }
      catch (ApplicationException ex)
      {
        return BadRequest(new
        {
          messaage = ex.Message
        });
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      locationService.Delete(id);
      return Ok();
    }
  }
}