using JS.Chat.Domain.Models;
using JS.Chat.Service.Interface;
using System;
using System.IO;
using System.Net;

namespace JS.Chat.Service.Service
{
    public class StockService: IStockService
    {
        public BotModel BotDetection(string message)
        {
            BotModel botResponse;            
            try
            {
                var stock_code = message.Replace("/stock=", "");
                HttpWebRequest myHttpReq = (HttpWebRequest)WebRequest.Create($"https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv");
               
                myHttpReq.KeepAlive = false;
                myHttpReq.Method = "GET";
                myHttpReq.AllowAutoRedirect = true;

                HttpWebResponse myHttpRes = (HttpWebResponse)myHttpReq.GetResponse();

                StreamReader sr = new StreamReader(myHttpRes.GetResponseStream());
                string result = sr.ReadToEnd();
                sr.Close();

                var data = result.Substring(result.IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
                var resultArray = data.Split(',');
                var stock = new StockModel()
                {
                    Symbol = resultArray[0],
                    Date = !resultArray[1].Contains("N/D") ? Convert.ToDateTime(resultArray[1]) : default,
                    Time = !resultArray[2].Contains("N/D") ? Convert.ToDateTime(resultArray[2]).TimeOfDay : default,
                    Open = !resultArray[3].Contains("N/D") ? Convert.ToDouble(resultArray[3]) : default,
                    High = !resultArray[4].Contains("N/D") ? Convert.ToDouble(resultArray[4]) : default,
                    Low = !resultArray[5].Contains("N/D") ? Convert.ToDouble(resultArray[5]) : default,
                    Close = !resultArray[6].Contains("N/D") ? Convert.ToDouble(resultArray[6]) : default,
                    Volume = !resultArray[7].Contains("N/D") ? Convert.ToDouble(resultArray[7]) : default,
                };
                botResponse = new BotModel { Detected = true, IsSuccessful = true, Symbol = stock.Symbol, Close = stock.Close.ToString("0.##") };
            }
            catch (Exception ex)
            {
                botResponse = new BotModel { Detected = true, IsSuccessful = false, ErrorMessage = ex.Message };
            }
            return botResponse;
        }
    }
}
