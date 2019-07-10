using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parser.Service;
using Parser.Models;

namespace Parser.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ParserController : ControllerBase, IParserController
  {

    IParser parser;

    public ParserController(IParser _parser) {
      parser = _parser;
    }

    [HttpGet]
    public ParseResponse Get()
    {
      // Debugging Purposes...Testing through typing API into url bar
      ParseRequest parseRequest = new ParseRequest();
      parseRequest.KeyWords = new List<string> { "InfoTrack" };
      parseRequest.CallWebAndWriteFlag = false;
      return parser.Execute(parseRequest);
    }

    [HttpPost]
    public ParseResponse Post([FromBody]ParseRequest parseRequest)
    {
      return parser.Execute(parseRequest);
    }

  }
}