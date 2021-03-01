using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Providers
{
    public class ConverterProvider
    {
        public async Task Convert()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            HttpResponseMessage result;
            using (HttpClient client = new HttpClient()) {
                result = await client.SendAsync(request); 
            }
            var temp = result.Content;
            //var xml = Json .Load("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            //var USD = xml.Elements("exchange").Elements("currency").FirstOrDefault(x => x.Element("cc").Value == CurrencyType.USD.ToString()).Elements("rate").FirstOrDefault().Value;
            //var EUR = xml.Elements("exchange").Elements("currency").FirstOrDefault(x => x.Element("cc").Value == CurrencyType.EUR.ToString()).Elements("rate").FirstOrDefault().Value;
            //GBP = 3,
            //CHF = 4,
            //JPY = 5,
            //UAH = 6,
        }
    }
}
