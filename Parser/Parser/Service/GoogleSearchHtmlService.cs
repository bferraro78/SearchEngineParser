using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Parser.Models;

namespace Parser.Service
{

    public class GoogleSearchHtmlService
    {

        private const string writtenHtmlResponse = @"ResultDLProd.xml";
        private static HttpClient client = new HttpClient();
        private bool realWebCall;
        private ParseRequest parseRequest;

        public GoogleSearchHtmlService(ParseRequest _parseRequest)
        {
            parseRequest = _parseRequest;
            realWebCall = _parseRequest.CallWebAndWriteFlag;
        }

        public string ExecuteWebCall()
        {
            if (realWebCall) WriteWebResponseToFile(CallWeb());
            return readHtmlCodeFromFile();
        }

        private string readHtmlCodeFromFile()
        {
            StreamReader sr = new StreamReader(writtenHtmlResponse);
            var htmlCode = sr.ReadToEnd();
            sr.Close();
            return htmlCode;
        }

        private string CallWeb()
        {
            string uri = "https://google.com.au/search?num=" + parseRequest.NumberOfResults + "&q=" + parseRequest.SearchValue;
            string response = Invoke(uri).Result;
            return response;
        }

        private void WriteWebResponseToFile(string webResponse)
        {
            using (StreamWriter outputFile = new StreamWriter(writtenHtmlResponse, false))
            {
                    outputFile.Write(webResponse);
            }
        }
        private async Task<string> Invoke(string uri)
        {
            System.IO.Stream m = await client.GetStreamAsync(uri);
            StreamReader reader = new StreamReader(m);
            string text = reader.ReadToEnd();
            m.Close();
            reader.Close();
            return text;
        }

    }
}