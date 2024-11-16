using System.Net;
using System.Net.Http.Headers;
using Framework.Core.Mediator;
using Framework.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stock_Market.Api.Api.ViewModels;
using Stock_Market.Api.Application.Commands.AddPriceStock;
using Stock_Market.Api.Application.Queries;
using Stock_Market.Api.Domain.Models.Entities.Ids;
using Stock_Market.Api.Infra.Data.Dtos;

namespace Stock_Market.Api.Api.Controllers
{
    [ApiController]
    [Route("api/price-stock/v1/")]
    //[OpenApiTag("Activity workers", Description = "Activity workers services")]
    public class PriceStockController(IMediatorHandler mediatorHandler, IPriceStockQuery priceStockQuery) : MainController
    {
        /// <summary>
        /// Purchase a stock
        /// </summary>

        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("Execute")]
        [ProducesResponseType(typeof(AddPriceStockCommandOutput),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Execute([FromBody] ExecuteStockViewModel viewModel)
        {
            var command = new AddPriceStockCommand(viewModel.StockSymbol);
            var commandHandlerOutput = await mediatorHandler.SendCommand<AddPriceStockCommand, AddPriceStockCommandOutput>(command);
            return CustomResponseStatusCodeCreated(commandHandlerOutput, $"api/price-stock/v1/{viewModel.StockSymbol}");
        }
        
        
        [HttpGet("{stockSymbol}")]
        [ProducesResponseType(
            typeof(PriceStockDto),
            (int)HttpStatusCode.OK)]
        [ProducesResponseType(
            typeof(ValidationProblemDetails),
            (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(
            typeof(ValidationProblemDetails),
            (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(
            typeof(ValidationProblemDetails),
            (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByActivityId(string stockSymbol)
        {

            return CustomResponseStatusCodeOk(await priceStockQuery.GetPriceStock(stockSymbol));

        }

    }
}
