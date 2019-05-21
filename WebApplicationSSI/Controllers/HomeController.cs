using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using SSIApi.Dto;
using SSIApi.Entities;
using WebApplicationSSI.Dtos;
using WebApplicationSSI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;

namespace WebApplicationSSI.Controllers
{
  public class HomeController : Controller
  {
    private static readonly RestClient client = new RestClient("https://localhost:44312/api/");

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
      bool loginSuccessfull = false;

      var request = new RestRequest("users/authenticate", Method.POST);
      request.RequestFormat = DataFormat.Json;
      request.AddJsonBody(new LoginDto { Username = model.Username, Password = model.password });
      var response = client.Execute(request);
      JObject responseContent = JObject.Parse(response.Content);

      loginSuccessfull = response.IsSuccessful;
      if (loginSuccessfull)
      {
        HttpContext.Session.SetString("bearerToken", responseContent.GetValue("token").ToString());
        return RedirectToAction("Index", "Data");
      }
      else
      {
        return View("Index", new LoginViewModel() { message = "Login Failed" });
      }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}