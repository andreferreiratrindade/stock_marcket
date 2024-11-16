namespace Stock_Market.Api.Infra.Data.Dtos;

public record PriceStockDto( 
    string Symbol,
    decimal Regular,
    decimal RegularDayHigh,
    decimal RegularDayLow,
    int RegularVolume,
    DateTime ReferenceTime);