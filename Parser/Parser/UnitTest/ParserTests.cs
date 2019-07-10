using Xunit;
using System;
using Parser.Service;
using Parser.Models;
using System.Collections.Generic;

public class ParserTests
{
    [Fact]
    public void DivParserExecuteTest()
    {
        IParser divParser = new DivParser();
        ParseRequest parserRequest = new ParseRequest
        {
            KeyWords = new List<string> {
                "InfoTrack",
                "Info",
                "protitle",
                "not present"
            },
            CallWebAndWriteFlag = false,
            NumberOfResults = 100,
            SearchValue = "online+title+search"
        };
        ParseResponse response = divParser.Execute(parserRequest);
        Assert.Equal(response.KeyWordSearchNumberDictionary["InfoTrack"][0], "76");
        Assert.Equal(response.KeyWordSearchNumberDictionary["Info"], new List<string> { "16", "22", "42", "76" });
        Assert.Equal(response.KeyWordSearchNumberDictionary["protitle"][0], "5");
        Assert.Equal(response.KeyWordSearchNumberDictionary["not present"][0], "0");


        Console.WriteLine("Testing complete");
    }

}