using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using System.IO;
using System.Security.AccessControl;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;

namespace proheto2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductInf productType = new ProductInf();

            List<string> urls = new List<string>
            {
                "https://www.mercadolivre.com.br/melannox-fluid-glow-skin-fluido-clareador-30ml-cosmobeauty/p/MLB19721373/s?pdp_filters=category:MLB264874",
                "https://www.mercadolivre.com.br/melano-defense-clareador-e-protetor-fps90-50g-biomarine/p/MLB21428521/s?pdp_filters=category:MLB199407",
                "https://www.mercadolivre.com.br/cosmoblock-blur-vitamina-c-fps98-cosmobeauty-50g-chocolate/p/MLB21717560/s?pdp_filters=category:MLB264874"
            };

            foreach (string url in urls)
            {
                var httpClient = new HttpClient();
                var html = httpClient.GetStringAsync(url).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var productElement = htmlDocument.DocumentNode.SelectSingleNode(
                    "//h1[@class='ui-pdp-title']"
                );
                productType.ProductName = productElement.InnerText.Trim();

                var productElement2 = htmlDocument.DocumentNode.SelectSingleNode(
                    "//span[@class='andes-money-amount__fraction']"
                );
                var Xprice = productElement2.InnerText.Trim();
                productType.Price = Xprice;

                var productElement3 = htmlDocument.DocumentNode.SelectSingleNode(
                    "//span[@class='ui-pdp-color--BLUE ui-pdp-family--REGULAR']"
                );
                productType.Seller = productElement3.InnerText.Trim();

                var melannoxFluidElement4 = htmlDocument.DocumentNode.SelectSingleNode(
                    "//p[@class='ui-pdp-family--REGULAR ui-pdp-media__title']"
                );
                productType.AdType = melannoxFluidElement4.InnerText.Trim();
                if (productType.AdType.Contains("sem juros"))
                {
                    productType.AdType = "Anuncio Premium";
                }
                else
                {
                    productType.AdType = "Anuncio Premium";
                }
                Console.WriteLine(
                    productType.ProductName
                        + " "
                        + productType.Seller
                        + " "
                        + productType.Price
                        + " "
                        + productType.AdType
                );
                Console.WriteLine("\n");
            }
        }
    }
}
