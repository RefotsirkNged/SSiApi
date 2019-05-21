using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SSIApi.Dto;
using SSIApi.Entities;
using SSIApi.Helpers;
using SSIApi.Interfaces;
using SSIApi.Services;

namespace SSIApi.Controllers
{
  [ResponseCache(NoStore = true, Duration = 0)]
  [Route("api/[controller]")]
  [ApiController]
  public class ArticlesController : ControllerBase
  {
    private IMapper mapper;
    private readonly AppSettings appSettings;
    private IArticleService articleService;

    public ArticlesController(IMapper _mapper, IOptions<AppSettings> _appSettings, IArticleService _articleService)
    {
      mapper = _mapper;
      appSettings = _appSettings.Value;
      articleService = _articleService;
    }

    [HttpPost]
    public IActionResult Register([FromBody]ArticleDto articleDto)
    {
      Article article = mapper.Map<Article>(articleDto);

      try
      {
        articleService.Create(article);
        return Ok(article);
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
      var articles = articleService.GetAll();
      var articleDtos = mapper.Map<IList<ArticleDto>>(articles);

      return Ok(articleDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var article = articleService.GetById(id);
      var articleDto = mapper.Map<ArticleDto>(article);
      return Ok(articleDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody]ArticleDto articleDto)
    {
      var article = mapper.Map<Article>(articleDto);
      article.ArticleId = id;

      try
      {
        articleService.Update(article);
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
      articleService.Delete(id);
      return Ok();
    }
  }
}