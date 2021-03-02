using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public class ConverterProviderOptions
        {
            public const string PATH_TO_API = "https://api.exchangeratesapi.io/latest?base=USD&symbols={0}";
            public const string ROOT = "rates";
            public const byte SYMBOLS_AFTER_COMMA = 2;
        }
    }
}
