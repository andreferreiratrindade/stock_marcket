using Stock_Market.Api.Infra.Data.ResponseModels;

namespace Stock_Market.Api.Domain.Models.Repositories;

public interface IBrApiRepository
{
    Task<BrApiStockResponseModel> GetStoke(string stockSymbol);
}