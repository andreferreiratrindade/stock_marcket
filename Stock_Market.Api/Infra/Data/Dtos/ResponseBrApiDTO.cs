namespace Stock_Market.Api.Infra.Data.Dtos;

public record ResponseBrApiDto( decimal Regular,
    decimal RegularDayHigh,
    decimal RegularDayLow,
    int RegularVolume,
    DateTime ReferenceTime);