using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Store.BusinessLogic.Exceptions;
using Store.Shared.Constants;
using Store.Shared.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Providers
{
    public class ConverterProvider
    {
        private readonly string _apiRoute;
        public ConverterProvider(IOptions<ServiceOptions> options)
        {
            _apiRoute = options.Value.ConverterRoute;
        }
        public async Task<double> ConvertAsync(CurrencyType type, double price)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_apiRoute, type));
            HttpResponseMessage requestResult;
            using (HttpClient client = new HttpClient())
            {
                requestResult = await client.SendAsync(request);
            }
            if (!requestResult.IsSuccessStatusCode)
            {
                throw new BusinessLogicException(ExceptionConsts.CURRENCY_CONVERT_PROBLEM);
            }
            var jObject = JObject.Parse(await requestResult.Content.ReadAsStringAsync());
            var rate = jObject[ConverterProviderConsts.ROOT][type.ToString()].Value<double>();
            var result = Math.Round(rate * price, ConverterProviderConsts.SYMBOLS_AFTER_COMMA);
            return result;
        }
    }
}
