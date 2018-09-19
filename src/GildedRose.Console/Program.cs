using System.Collections.Generic;
using GildedRose.Console.Items;

namespace GildedRose.Console
{
    class Program
    {
        internal const int MaxQuality = 50;

        IList<Item> Items;

        static void Main()
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            UpdateQuality(app.Items);

            System.Console.ReadKey();

        }

        public static void UpdateQuality(IEnumerable<Item> items)
        {
            foreach (Item item in items)
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
