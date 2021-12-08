
using System;
using System.IO;
using System.Net;



namespace BitCoinCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            BitCoinRate currentBitCoin = GetRates();
            Console.WriteLine("Enter the amount of bitcoin");
            float userCoins = float.Parse(Console.ReadLine());
            Console.WriteLine("select currency: EUR/USD/GBS");
            string userCurrency = Console.ReadLine().ToUpper();

            float currentCoinRate = 0;
            string currencyCode = "";

            if (userCurrency == "EUR")
            {
                currentCoinRate = currentBitCoin.bpi.EUR.rate_float;
            }else if (userCurrency == "USD")
            {
                currentCoinRate = currentBitCoin.bpi.USD.rate_float;
                currencyCode = currentBitCoin.bpi.USD.code; 
            }else if (userCurrency == "GBS")
            {
                currentCoinRate = currentBitCoin.bpi.USD.rate_float;
                currencyCode = currentBitCoin.bpi.USD.code; 
            }
            float result = userCoins * currentCoinRate;

            Console.WriteLine("$Your bitcoins {result} {currencyCode} are worth ");

           //Console.WriteLine($"current rate: {currentBitCoin.bpi.USD.code} {currentBitCoin.bpi.USD.rate_float}");
            Console.WriteLine($"{currentBitCoin.disclaimer}");
        }

        public static BitCoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitCoinRate bitCoinData;

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitCoinData = JsonConvert.DeserializeObject<BitCoinRate>(response);
            }

            return bitCoinData;
        }
    }
}
