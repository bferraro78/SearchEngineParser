using System.Collections.Generic;
using System.Threading.Tasks;
using Parser.Models;

namespace Parser.Service {

  public interface IParser {
    
    ParseResponse Execute(ParseRequest parseRequest);

  }
}