using Parser.Models;

namespace Parser.Controllers {
  public interface IParserController {
    ParseResponse Get();
  }
}