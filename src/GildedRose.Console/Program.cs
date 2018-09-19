using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {
        public const string BackStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        public const string AgedBrieName = "Aged Brie";
        public const string SulfurasName = "Sulfuras, Hand of Ragnaros";
        public const string ConjuredName = "Conjured Mana Cake";
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
                                              new Item {Name = AgedBrieName, SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = SulfurasName, SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = BackStagePassName,
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = ConjuredName, SellIn = 3, Quality = 6}
                                          }

                          };

            UpdateQuality(app.Items);

            System.Console.ReadKey();

        }

        public static void UpdateQuality(IEnumerable<Item> items)
        {
            foreach (Item item in items)
            {
                if (item.Name != AgedBrieName && item.Name != BackStagePassName)
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name == ConjuredName)
                        {
                            item.Quality = item.Quality - 2;
                        }
                        else if (item.Name != SulfurasName)
                        {
                            item.Quality = item.Quality - 1;
                        }
                    }
                }
                else
                {
                    if (item.Quality < MaxQuality)
                    {
                        item.Quality = item.Quality + 1;

                        if (item.Name == BackStagePassName)
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.Quality < MaxQuality)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.Quality < MaxQuality)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (item.Name != SulfurasName)
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn < 0)
                {
                    if (item.Name != AgedBrieName)
                    {
                        if (item.Name != BackStagePassName)
                        {
                            if (item.Quality > 0)
                            {
                                if (item.Name != SulfurasName)
                                {
                                    item.Quality = item.Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            item.Quality = item.Quality - item.Quality;
                        }
                    }
                    else
                    {
                        if (item.Quality < MaxQuality)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }
            }
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
