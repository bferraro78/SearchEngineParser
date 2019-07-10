var ParserViewModel = {
    searchValue: ko.observable("online title search"),
    keyWords: ko.observable("InfoTrack,Info,www.infotrack.com.au"),
    numSearches: ko.observable("100"), // Not in use..next Implemented Feature
    getNewSearchResults: ko.observable(false),
    FetchingResults: ko.observable(true),
    VisibleResults: ko.observable(false),
    SearchResults: ko.observableArray(),
    DebugText: ko.observable(""),
    getResults: function () {
        callWebServer();
    }
};

var callWebServer = function () {

    ParserViewModel.DebugText("Fetching Results...");
    ParserViewModel.FetchingResults(false);
    ParserViewModel.VisibleResults(false);

    // Form Request Object
    var newSearchValue = searchValueFilter(ParserViewModel.searchValue());
    var KeyWordList = createKeyWordList(ParserViewModel.keyWords());

    var Request = {
        KeyWords: KeyWordList,
        SearchValue: newSearchValue,
        NumberOfResults: 100,
        CallWebAndWriteFlag: ParserViewModel.getNewSearchResults
    };

    var JsonRequest = ko.toJSON(Request);
    var uri = "https://localhost:5001/api/Parser";

    // Make POST AJAX Call
    $.ajax({
        type: 'POST',
        url: uri,
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        data: JsonRequest,
        success: function (response) {
            console.log(response);
            ParserViewModel.DebugText("Done!");
            ParserViewModel.FetchingResults(true);
            var responseBindableArray = mapDictionaryToArray(response.keyWordSearchNumberDictionary);
            console.log(responseBindableArray);
            ParserViewModel.SearchResults(responseBindableArray);
            ParserViewModel.VisibleResults(true);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("Too many Requests to Google as a result of not using their API...Uncheck the checkbox.");
            ParserViewModel.DebugText("There may have been too many requests sent to Google...Try unchecking the checkbox and making sure the server is running.");
            ParserViewModel.FetchingResults(true);
            ParserViewModel.VisibleResults(false);
        }
    });

};

var mapDictionaryToArray = function (dictionary) {
    var result = [];
    for (var key in dictionary) {
        result.push({ SearchValue: key, SearchValueResultNumbers: dictionary[key] });
    }
    return result;
}

var searchValueFilter = function (searchValue) {
    var searchStr = searchValue + '';
    searchStr = searchStr.replace(/[/]/g, '').replace(/[\\]/g, '').replace(/[+]/g, '');
    searchStr = searchStr.replace(/[ ]/g, '+');
    return searchStr;
}

var createKeyWordList = function (keyWordStr) {
    return keyWordStr.split(",");
}

var filterNumberOfSearches = function (numSearches) {
    numSearches = numSearches.replace(/\D/g, '');
    return parseInt(numSearches, 10);
}


ko.applyBindings(ParserViewModel); // This makes Knockout get to work