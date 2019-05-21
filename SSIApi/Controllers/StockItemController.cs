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
  [ApiController]
  [Route("api/[controller]")]
  public class StockItemsController : ControllerBase
  {
    private IMapper mapper;
    private readonly AppSettings appSettings;
    private IStockItemService stockItemService;

    public StockItemsController(IMapper _mapper, IOptions<AppSettings> _appSettings, IStockItemService _stockItemService)
    {
      mapper = _mapper;
      appSettings = _appSettings.Value;
      stockItemService = _stockItemService;
    }

    [HttpPost]
    public IActionResult Create([FromBody]StockItemDto stockItemDto)
    {
      StockItem stockItem = mapper.Map<StockItem>(stockItemDto);
      try
      {
        stockItemService.Create(stockItem);
        return Ok(stockItem);
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
      var stockItems = stockItemService.GetAll();
      var stockItemDtos = mapper.Map<IList<StockItemDto>>(stockItems);

      return Ok(stockItemDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var stockItem = stockItemService.GetById(id);
      var stockItemDto = mapper.Map<StockItemDto>(stockItem);
      return Ok(stockItemDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody]StockItemDto stockItemDto)
    {
      var stockItem = mapper.Map<StockItem>(stockItemDto);
      stockItem.StockItemId = id;

      try
      {
        stockItemService.Update(stockItem);
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
      stockItemService.Delete(id);
      return Ok();
    }
  }
}