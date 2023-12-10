using classes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.IO;
using CsvHelper.Configuration.Attributes;
using Microsoft.SqlServer.Server;
using System.Threading;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ProjectFunctions
{

    internal class Functions
    {
        public Dictionary<string, string> Info = new Dictionary<string, string>();
        public List<string> Dados = new List<string>();
        public string op = "";


        public void GetDocInfo()
        {
            var all = File.ReadAllLines(@"C:\Users\rafael\Documents\GitHub\MarketGetPrices\proheto2\urls.txt");

            foreach (var line in all)
            {
                var lines = line.Split(',');
                Info.Add(lines[0], lines[1]);
            };
        }
        public void ReadCsvDocument()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(@"C:\Users\rafael\Documents\GitHub\MarketGetPrices\proheto2\archive.csv"))
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
            List<string> urls = new List<string>();
            var tList = new List<ProductInf>();

            var allLines = File.ReadAllLines(@"C:\\Users\\rafael\\Documents\\GitHub\\MarketGetPrices\\proheto2\\urls.txt");
        
            foreach (KeyValuePair<string, string> kvp in Info)
            {           
              Console.WriteLine($"{kvp.Key},{kvp.Value}");

                var httpClient = new HttpClient();
                var html = httpClient.GetStringAsync(kvp.Value).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                  
                var productElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='ui-pdp-title']");
                productType.ProductName = productElement.InnerText.Trim();
                
                var productElement2 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
                
                var Xprice = productElement2.InnerText.Trim();
                productType.Price = Xprice;
             
                var productElement3 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='ui-pdp-color--BLUE ui-pdp-family--REGULAR']");
                
                productElement3 = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='ui-pdp-color--BLUE ui-pdp-family--REGULAR']");
                productType.Seller = productElement3.InnerText.Trim();
                
                try
                {

                    // Select the desired element
                     htmlDocument.DocumentNode.SelectSingleNode("//p[@class='ui-pdp-family--REGULAR ui-pdp-media__title']");

                    // Check if the element exists
                    if (htmlDocument.DocumentNode.SelectSingleNode("//p[@class='ui-pdp-family--REGULAR ui-pdp-media__title']") != null)
                    {
                        productType.AdType = "Anuncio Premium";
                    }
                    else
                    {
                        productType.AdType = "Anuncio Classico";
                    }
                    //Console.WriteLine(productType.AdType);
                }
                catch (Exception)
                {
                    Console.WriteLine("não encontrado");
                }

                //Console.WriteLine($"{productType.ProductName},{productType.Price},{productType.Seller},{productType.AdType}\n");


                tList.Add(new ProductInf() { ProductName = productType.ProductName, Price = productType.Price, Seller = productType.Seller, AdType = productType.AdType });


                using (var writer = new StreamWriter(@"C:\Users\rafael\Documents\GitHub\MarketGetPrices - Copia\proheto2\Tabela.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                 
                    csv.WriteRecords(tList);

                }
                
            }
        }

        public void GetClientlinks()
        {
            Console.WriteLine("Insira os sku's e os links que desejares");

            string line;
            while ((line = Console.ReadLine()) != "")
            {
                Dados.Add(line);
            };

            foreach (var item in Dados)
            {
                string[] data = item.Split(',');

                try
                {
                    Info.Add(data[0], data[1]);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Um produto com o Sku = {data[0]} Já foi adicionado");

                };

            }

            Console.WriteLine("\n\n");

            foreach (KeyValuePair<string, string> kvp in Info)
            {
                Console.WriteLine($"{kvp.Key},{kvp.Value}");
            };
            using (var file = File.CreateText(@"C:\Users\rafael\Documents\GitHub\MarketGetPrices\proheto2\urls.txt"))
            {
                foreach (KeyValuePair<string, string> kvp in Info)
                {
                    file.WriteLine($"{kvp.Key},{kvp.Value}");// Substitui o conteúdo do arquivo com este texto
                };

                file.Close();
                Console.WriteLine("\ngostaria de refazer o processo ? ");
                op = Console.ReadLine();
            }
        }        
    }
} 


        
   
