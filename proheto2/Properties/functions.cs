﻿using classes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.IO;
using CsvHelper.Configuration.Attributes;

namespace ProjectFunctions
{
    internal class Functions
    {
        public void AddUrls()
        {
            Dictionary<string, UrLinfo> Urls = new Dictionary<string, UrLinfo>();
            //ler o que tem dentro do documento e comparar com os novos inputs. Se for diferente, adicionar no dictionary
            string[] all = File.ReadAllLines(@"C:\Users\rafael\source\repos\proheto2\proheto2\urls.txt");
            Console.WriteLine("insira as urls, skus e o tipo do anuncio");
            string newAll = Console.ReadLine();
            string[] linhas = newAll.Split('\n');

            
            } 

        public ProductInf DataSearch()

        {
            // usuario precisa adicionar e retirar as urls das listas. Usar arquivo txt?

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

                var productElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='ui-pdp-title']");
                productType.ProductName = productElement.InnerText.Trim();

                var productElement2 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
                var Xprice = productElement2.InnerText.Trim();
                productType.Price = Xprice;

                var productElement3 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='ui-pdp-color--BLUE ui-pdp-family--REGULAR']");
                productType.Seller = productElement3.InnerText.Trim();

                var melannoxFluidElement4 = htmlDocument.DocumentNode.SelectSingleNode("//p[@class='ui-pdp-family--REGULAR ui-pdp-media__title']");
                productType.AdType = melannoxFluidElement4.InnerText.Trim();
                //if (productType.AdType.Contains("sem juros"))
                {
                    productType.AdType = "Anuncio Premium";
                }
                //else
                {
                    productType.AdType = "Anuncio Premium";
                }
                //Console.WriteLine(productType.ProductName + " " + productType.Seller + " " + productType.Price + " " + productType.AdType);
                //Console.WriteLine("\n");
            }
            return productType;
        }
        public void ReadCsvDocument()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(@"C:\Users\rafael\source\repos\proheto2\proheto2\archive.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<ProductMap>();
                var datas = csv.GetRecords<ProductInf>();
               
                foreach (var data in datas)
                {
                    Console.WriteLine($"{data.ProductName} ,{data.Seller}, {data.Price}, {data.AdType}");
                }
            }
        }
        public void WriteCsvDocument()
        {
     

            ProductInf productType = new ProductInf();
            List<string> urls = new List<string>
            {
            "https://www.mercadolivre.com.br/melannox-fluid-glow-skin-fluido-clareador-30ml-cosmobeauty/p/MLB19721373/s?pdp_filters=category:MLB264874",
            "https://www.mercadolivre.com.br/melano-defense-clareador-e-protetor-fps90-50g-biomarine/p/MLB21428521/s?pdp_filters=category:MLB199407",
            "https://www.mercadolivre.com.br/cosmoblock-blur-vitamina-c-fps98-cosmobeauty-50g-chocolate/p/MLB21717560/s?pdp_filters=category:MLB264874"
            };

            var tList = new List<ProductInf>();
            foreach (string url in urls)
            {

                var httpClient = new HttpClient();
                var html = httpClient.GetStringAsync(url).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var productElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='ui-pdp-title']");
                productType.ProductName = productElement.InnerText.Trim();

                var productElement2 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
                var Xprice = productElement2.InnerText.Trim();
                productType.Price = Xprice;

                var productElement3 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='ui-pdp-color--BLUE ui-pdp-family--REGULAR']");
                productType.Seller = productElement3.InnerText.Trim();

                var melannoxFluidElement4 = htmlDocument.DocumentNode.SelectSingleNode("//p[@class='ui-pdp-family--REGULAR ui-pdp-media__title']");
                productType.AdType = melannoxFluidElement4.InnerText.Trim();
                if (productType.AdType.Contains("sem juros"))
                {
                    productType.AdType = "Anuncio Premium";
                }
                else
                {
                    productType.AdType = "Anuncio Premium";
                }


                Console.Write($"{productType.ProductName},{productType.Price},{productType.Seller},{productType.AdType}\n");
                

                tList.Add(new ProductInf() { ProductName = productType.ProductName,Price = productType.Price,Seller = productType.Seller ,AdType = productType.AdType });


                using (var writer = new StreamWriter(@"C:\Users\rafael\source\repos\proheto2\proheto2\archive.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(tList);
                    
                }
iu
            } 
        }
    }
}
        
   