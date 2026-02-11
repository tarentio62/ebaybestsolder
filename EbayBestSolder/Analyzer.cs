using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EbayBestSolder
{
    public class Analyzer
    {

        public async Task<List<Item>> Run(string url)
        {
            List<Item> items = new List<Item>();
            var html = new HtmlDocument();
            var httpClient = new HttpClient();


            for (int i = 200; i < 800; i += 200)
            {

           
                var response = await httpClient.GetAsync(new Uri(url+i));

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                html.LoadHtml(content);
                var root = html.DocumentNode;

                var listElement = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("sresult lvresult clearfix li shic"));


                foreach (var element in listElement.Where(e => e.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("hotness-signal red")).Count() > 0))
                {
                    try
                    {

                    var num = element.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("hotness-signal red")).FirstOrDefault();
                      
                    var item = new Item();
                    item.Count = int.Parse(Regex.Match(num.InnerText, @"\d+").Value);
                    item.Url = element.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                    item.Name = element.Descendants("h3").FirstOrDefault().InnerText;
                    if (num.InnerText.Contains("vendus"))
                        item.BestOn = ItemMode.Sold;
                    else
                        item.BestOn = ItemMode.Follow;

                    if (!items.Exists(it=>it.Url == item.Url))
                        items.Add(item);


                    }
                    catch (Exception ex)
                    {

                        continue;
                    }
                }
            }

            return items;

        }

        public bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }


}
