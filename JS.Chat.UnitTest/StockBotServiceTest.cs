using JS.Chat.Service.Service;
using Xunit;

namespace JS.Chat.UnitTest
{
    public class StockBotServiceTest
    {
        [Fact]
        public void GetStock()
        {
            StockService stockService = new StockService();
            var stock_code = "/stock=aapl.us";
            var result = stockService.BotDetection(stock_code);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetStockBadRequest()
        {
            StockService stockService = new StockService();
            var stock_code = "/stock=-------233$$$a''''eeel***wewe%%%**?//*/dadad????";
            var result = stockService.BotDetection(stock_code);
            Assert.NotNull(result);
        }
    }
}
