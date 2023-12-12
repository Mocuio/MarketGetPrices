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
using classes;
using ProjectFunctions;

namespace projeto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Functions pg = new Functions();
           pg.GetDocInfo();
           pg.GetClientlinks();
           pg.WriteCsvDocument();
           
            //pg.ReadCsvDocument();
         
        }
    }
}

