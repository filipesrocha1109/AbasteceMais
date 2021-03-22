using System.Collections.Generic;

namespace AbasteceMais.Domain.Common
{
    public class Config
    {
        public IList<string> DateFormats { get; set; }

        public int NumberFilesISend { get; set; }

        public string ResponseDateFormat { get; set; }

        public IDictionary<string, string> CityIsend { get; set; }

        public IDictionary<string, string> FieldISend { get; set; }

        public string UrlContactISend { get; set; }

        public string UrlCustomFieldISend { get; set; }

        public string UrlLoginIsend { get; set; }
    }
}
