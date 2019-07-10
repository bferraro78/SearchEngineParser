using System;
using System.Collections.Generic;

namespace Parser.Models
{
  public class ParseResponse
  {
    public Dictionary<string, List<string>> KeyWordSearchNumberDictionary { get; set; }

    public ParseResponse() {
      KeyWordSearchNumberDictionary = new Dictionary<string, List<string>>();
    }
  }
}