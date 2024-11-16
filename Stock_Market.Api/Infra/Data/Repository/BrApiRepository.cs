using System.Text.Json;
using Google.Protobuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stock_Market.Api.Domain.Models.Repositories;
using Stock_Market.Api.Infra.Data.ResponseModels;

namespace Stock_Market.Api.Infra.Data.Repository;

public class BrApiRepository(IHttpClientFactory clientFactory, ILogger<BrApiRepository> logger)
    : IBrApiRepository
{
    
    public async Task<BrApiStockResponseModel> GetStoke(string stockSymbol)
    {
        var endpointUrl = new Uri($"https://brapi.dev/api/quote/{stockSymbol}");
        try
        {
            using (var client = clientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer 66nP9bCHjCTpTftTVbypGy");
                client.BaseAddress = endpointUrl;
                var response = await client.GetAsync(endpointUrl);
                response.EnsureSuccessStatusCode();

                string contentString = await response.Content.ReadAsStringAsync();
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(contentString);
                //var responseJson = JObject.Parse(contentString).ToObject<BrApiStockResultResponseModel>();
                    var brApistockResponse =  myDeserializedClass.results
                                    .Select(x=> 
                                            new BrApiStockResponseModel( x.regularMarketPrice,
                                                x.regularMarketDayHigh, x.regularMarketDayLow, x.regularMarketVolume,
                                                x.regularMarketTime)).ToList();

                    return brApistockResponse.FirstOrDefault();

            }
        }
        catch (HttpRequestException ex)
        {
            logger.LogError($"An error occurred getting all countries: {ex}");
            throw;
        }
    }
   
}