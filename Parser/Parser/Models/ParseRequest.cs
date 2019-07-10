using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Parser.Models
{
    [JsonObject]
  public class ParseRequest
  {
    [JsonProperty]
    public List<string> KeyWords { get; set; }
    
    [JsonProperty]
    public int NumberOfResults { get; set; }

    [JsonProperty]
    public string SearchValue { get; set; }

    [JsonProperty]
    public bool CallWebAndWriteFlag;

  }
}



