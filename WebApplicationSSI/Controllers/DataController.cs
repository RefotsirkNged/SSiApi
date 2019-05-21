using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApplicationSSI.Models;
using Microsoft.AspNetCore.Http;
using SSIApi.Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SSIApi.Dto;
using AutoMapper;

namespace WebApplicationSSI.Controllers
{
  public class DataController : Controller
  {
    private static readonly RestClient client = new RestClient("https://localhost:44312/api/");

    public IActionResult Index()
    {
      var token = HttpContext.Session.GetString("bearerToken");
      var request = new RestRequest("users/", Method.GET);
      request.AddHeader("Authorization", "Bearer " + token);
      var response = client.Execute(request);
      var responseContent = response.Content;
      List<UserDto> userDtos = JsonConvert.DeserializeObject<List<UserDto>>(responseContent);

      request = new RestRequest("articles/", Method.GET);
      request.AddHeader("Authorization", "Bearer " + token);
      response = client.Execute(request);
      responseContent = response.Content;
      List<ArticleDto> articleDtos = JsonConvert.DeserializeObject<List<ArticleDto>>(responseContent);

      request = new RestRequest("stockitems/", Method.GET);
      request.AddHeader("Authorization", "Bearer " + token);
      response = client.Execute(request);
      responseContent = response.Content;
      List<StockItemDto> stockItemsDtos = JsonConvert.DeserializeObject<List<StockItemDto>>(responseContent);

      request = new RestRequest("locations/", Method.GET);
      request.AddHeader("Authorization", "Bearer " + token);
      response = client.Execute(request);
      responseContent = response.Content;
      List<LocationDto> locationsDtos = JsonConvert.DeserializeObject<List<LocationDto>>(responseContent);

      var users = Mapper.Map<IList<User>>(userDtos);
      var articles = Mapper.Map<IList<Article>>(articleDtos);
      var stockItems = Mapper.Map<IList<StockItem>>(stockItemsDtos);
      var locations = Mapper.Map<IList<Location>>(locationsDtos);

      return View(new DataViewModel { Users = (List<User>)users, Articles = (List<Article>)articles, StockItems = (List<StockItem>)stockItems, Locations = (List<Location>)locations });
    }

    public IActionResult SubmitArticle(DataViewModel model)
    {
      var token = HttpContext.Session.GetString("bearerToken");
      var request = new RestRequest("articles/create", Method.POST);
      request.AddHeader("Authorization", "Bearer " + token);
      request.RequestFormat = DataFormat.Json;
      request.AddJsonBody(new ArticleDto { ArticleId = 0, Name = model.newArticle.Name, StockItemId = model.newArticle.StockItemId });
      var response = client.Execute(request);
      JObject responseContent = JObject.Parse(response.Content);
      return Index();
    }

    public IActionResult SubmitStockItem(DataViewModel model)
    {
      var token = HttpContext.Session.GetString("bearerToken");
      var request = new RestRequest("users/", Method.GET);
      return Index();
    }

    public IActionResult SubmitUser(DataViewModel model)
    {
      var token = HttpContext.Session.GetString("bearerToken");
      var request = new RestRequest("users/", Method.GET);
      return Index();
    }

    public IActionResult SubmitLocation(DataViewModel model)
    {
      var token = HttpContext.Session.GetString("bearerToken");
      var request = new RestRequest("users/", Method.GET);
      return Index();
    }
  }
}