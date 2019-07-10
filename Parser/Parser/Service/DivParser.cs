using System.Collections.Generic;
using Parser.Models;

namespace Parser.Service
{

    public class DivParser : IParser
    {
        public ParseResponse Execute(ParseRequest parseRequest)
        {
            GoogleSearchHtmlService htmlService = new GoogleSearchHtmlService(parseRequest);
            string htmlCode = htmlService.ExecuteWebCall();
            ParseResponse parseResponse = ExecuteParse(parseRequest.KeyWords, htmlCode);
            return parseResponse;
        }

        protected ParseResponse ExecuteParse(List<string> listOfKeyWords, string htmlCode)
        {
            ParseResponse parseResponse = searchListOfGoogleSearchDivs(htmlCode, listOfKeyWords);
            fillInNonResults(parseResponse, listOfKeyWords);
            return parseResponse;
        }

        private ParseResponse fillInNonResults(ParseResponse parseResponse, List<string> listOfKeyWords)
        {
            for (var i = 0; i < listOfKeyWords.Count; i++)
            {
                var keyWord = listOfKeyWords[i];
                if (!parseResponse.KeyWordSearchNumberDictionary.ContainsKey(keyWord))
                {
                    parseResponse.KeyWordSearchNumberDictionary[keyWord] = new List<string> { "0" };
                }
            }
            return parseResponse;
        }

        private ParseResponse searchListOfGoogleSearchDivs(string html, List<string> listOfKeyWords)
        {
            ParseResponse parseResponse = new ParseResponse();
            string googleSearchDivTmp = "";
            int currentHtmlStringIndex = 0;
            int currentGoogleSearchNumber = 1;

            /* This function parsers HTMl for a certain Div class, that breaks up 
               each Search Result Accoridng to Google's Generic HTML */
            while (currentHtmlStringIndex != -1)
            {
                currentHtmlStringIndex = html.IndexOf(@"<div class=""ZINbbc xpd O9g5cc uUPGi""", currentHtmlStringIndex);
                if (currentHtmlStringIndex == -1) continue;
                int endIndex = html.IndexOf(@"<div class=""ZINbbc xpd O9g5cc uUPGi""", currentHtmlStringIndex + 1); // add 1 so it does re-find same div section
                if (endIndex == -1) break; // Last section will always be footer nonsense
                googleSearchDivTmp = html.Substring(currentHtmlStringIndex, endIndex - currentHtmlStringIndex);
                if (checkIfValidSearchSection(googleSearchDivTmp))
                {
                    findKeyWordsInSearchSection(parseResponse, listOfKeyWords, googleSearchDivTmp, currentGoogleSearchNumber); // Update Response Dictionary
                    currentGoogleSearchNumber++;
                }
                currentHtmlStringIndex = endIndex;
            }
            return parseResponse;
        }

        private void findKeyWordsInSearchSection(ParseResponse parseResponse, List<string> keyWords, string googleSearchDivSection, int googleSearchDivSectionNumber)
        {
            for (var i = 0; i < keyWords.Count; i++)
            {
                string keyWord = keyWords[i];
                if (googleSearchDivSection.IndexOf(keyWord) != -1)
                {
                    if (!parseResponse.KeyWordSearchNumberDictionary.ContainsKey(keyWord))
                    {
                        parseResponse.KeyWordSearchNumberDictionary[keyWord] = new List<string>();
                    }
                    parseResponse.KeyWordSearchNumberDictionary[keyWord].Add(googleSearchDivSectionNumber.ToString());
                    continue;
                }
            }

        }

        private bool checkIfValidSearchSection(string googleSearchDivTmp)
        {
            return googleSearchDivTmp.IndexOf(@"<div class=""BNeawe vvjwJb AP7Wnd""") != -1;
        }

    }
}