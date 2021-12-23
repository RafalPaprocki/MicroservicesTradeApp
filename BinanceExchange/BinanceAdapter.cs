using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Authentication;

namespace BinanceExchange
{
    public class BinanceAdapter
    {
        public async Task<BinanceOrderBook> GetOpenOrders()
        {
            var client = new BinanceClient(new BinanceClientOptions(){ 
            });
            var callResult = await client.Spot.Market.GetOrderBookAsync("ETHUSDT", 5000);
            if (callResult.Success)
            {
                return callResult.Data;
            }
            return null;
        }

        public async Task<string> CreateOrder(ApiCredentials credentials, string market, string transactionSide, decimal price, decimal amount )
        {
            var orderSide = OrderSide.Sell;
            if (transactionSide.Equals("Buy", StringComparison.InvariantCultureIgnoreCase)) {
                orderSide = OrderSide.Buy;
            }
            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = credentials
            });

            var result = await client.Spot.Order.PlaceOrderAsync(market, orderSide, OrderType.Limit,
                quantity: amount, price: price, timeInForce: TimeInForce.GoodTillCancel);

            if (result.Success)
            {
                return "Created";
            }
            else
            {
                return result.Error?.Message;
            } 
        } 
    }
}