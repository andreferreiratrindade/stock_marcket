namespace Stock_Market.Api.Infra.Data.ResponseModels;


public record BrApiStockResponseModel( decimal Regular,
    decimal RegularDayHigh,
    decimal RegularDayLow,
    int RegularVolume,
    DateTime ReferenceTime);
    
    public record BrApiStockResultResponseModel(List<BrApiStockResponseModel> Result);
    
    public class Result
    {
        public string currency { get; set; }
        public string shortName { get; set; }
        public string longName { get; set; }
        public decimal regularMarketChange { get; set; }
        public decimal regularMarketChangePercent { get; set; }
        public DateTime regularMarketTime { get; set; }
        public decimal regularMarketPrice { get; set; }
        public decimal regularMarketDayHigh { get; set; }
        public string regularMarketDayRange { get; set; }
        public decimal regularMarketDayLow { get; set; }
        public int regularMarketVolume { get; set; }
        public decimal regularMarketPreviousClose { get; set; }
        public decimal regularMarketOpen { get; set; }
        public string fiftyTwoWeekRange { get; set; }
        public decimal fiftyTwoWeekLow { get; set; }
        public decimal fiftyTwoWeekHigh { get; set; }
        public string symbol { get; set; }
        public decimal priceEarnings { get; set; }
        public decimal earningsPerShare { get; set; }
        public string logourl { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public DateTime requestedAt { get; set; }
        public string took { get; set; }
    }