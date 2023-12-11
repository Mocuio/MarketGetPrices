using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace classes
{
    public class ProductInf
    {
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Seller { get; set; }
        public string AdType { get; set; }
     
    }

    public class ProductMap : ClassMap<ProductInf>
    {
        public ProductMap()
        {
            Map(p => p.ProductName).Name("ProductName");
            Map(p => p.Price).Name("Price");
            Map(p => p.Seller).Name("Seller");
            Map(p => p.AdType).Name("AdType");
            Map(p => p.Sku).Name("Sku");
        }
    }

    public class UrLinfo
    {
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
