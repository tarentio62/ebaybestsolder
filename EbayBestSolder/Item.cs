using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayBestSolder
{
    public class Item
    {
        public string Name { get; set; }
        public ItemMode BestOn { get; set; }
        public int Count { get; set; }
        public string Url { get; set; }
    }

    public enum ItemMode
    {
        Sold,
        Follow
    }
}
