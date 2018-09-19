using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {
        public const string BackStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        public const string AgedBrieName = "Aged Brie";
        public const string SulfurasName = "Sulfuras, Hand of Ragnaros";
        public const string ConjuredName = "Conjured Mana Cake";

        IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
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

        public static void UpdateQuality(IList<Item> Items)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != AgedBrieName && Items[i].Name != BackStagePassName)
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name == ConjuredName)
                        {
                            Items[i].Quality = Items[i].Quality - 2;
                        }
                        else if (Items[i].Name != SulfurasName)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == BackStagePassName)
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != SulfurasName)
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != AgedBrieName)
                    {
                        if (Items[i].Name != BackStagePassName)
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != SulfurasName)
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
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
