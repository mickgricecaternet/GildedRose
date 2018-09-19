using System.Collections.Generic;
using System.Linq;
using GildedRose.Console.Items;

namespace GildedRose.Console
{
    class Program
    {
        internal const int MaxQuality = 50;

        IList<Item> Items;

        private Program()
        {
            // Empty constructor to force factory pattern using method CreateProgram
        }

        static void Main()
        {
            System.Console.WriteLine("OMGHAI!");

            var app = CreateProgram();

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public static Program CreateProgram(IEnumerable<Item> items = null)
        {
            return new Program
            {
                Items = items?.ToList() ?? new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new BrieItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new SulfurasItem {Name = "Sulfuras, Hand of Ragnaros" },
                    new BackstagePassItem
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new ConjuredItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                if (item is IComplexItem)
                {
                    (item as IComplexItem).UpdateQuality();
                }
                else
                {
                    // Default behaviour
                    if (item.SellIn <= 0)
                    {
                        item.Quality -= 2;
                    }
                    else
                    {
                        item.Quality--;
                    }

                    if (item.Quality < 0)
                    {
                        item.Quality = 0;
                    }
                }

                item.SellIn--;
            }
        }
    }
}
