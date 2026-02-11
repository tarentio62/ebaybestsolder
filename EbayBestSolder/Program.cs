using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayBestSolder
{
    class Program
    {
        static void Main(string[] args)
        {

            Analyzer ana = new Analyzer();

            var items = ana.Run("http://www.ebay.fr/sch/Maquillage/31786/i.html?Marque=%252D%2520Sans%2520marque%252FG%25C3%25A9n%25C3%25A9rique%2520%252D&_dcat=31786&LH_ItemCondition=1000&LH_BIN=1&_ipg=").Result;

            Console.WriteLine("Number of announce:" + items.Count);
            Console.WriteLine("");
            Console.WriteLine("");

            foreach (var item in items.OrderByDescending(i=>i.Count))
            {
                Console.WriteLine(item.Count + " " + item.BestOn.ToString() + " " + item.Name);
            }
            Console.ReadKey();
        }
    }
}
