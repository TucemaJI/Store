using Newtonsoft.Json.Linq;
using Store.BusinessLogic.Exceptions;
using Store.Shared.Constants;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Providers
{
    public class ConverterProvider
    {
        public async Task<double> ConvertAsync(CurrencyType type, double price)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(ConverterProviderOptions.PATH_TO_API, type));
            HttpResponseMessage requestResult;
            using (HttpClient client = new HttpClient())
            {
                requestResult = await client.SendAsync(request);
            }
            if (!requestResult.IsSuccessStatusCode)
            {
                throw new BusinessLogicException(ExceptionOptions.CURRENCY_CONVERT_PROBLEM);
            }
            var jObject = JObject.Parse(await requestResult.Content.ReadAsStringAsync());
            var rate = jObject[ConverterProviderOptions.ROOT][type.ToString()].Value<double>();
            var result = Math.Round(rate * price, ConverterProviderOptions.SYMBOLS_AFTER_COMMA);
            return result;
        }
    }
}
