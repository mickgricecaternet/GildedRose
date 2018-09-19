using System;
using System.Collections.Generic;
using GildedRose.Console;
using GildedRose.Console.Items;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        private const int InitialTestSellIn = 2;
        private const int InitialTestQuantity = 10;

        [Fact]
        public void TestUpdateQualityLowersSellInAndQuality()
        {
            Item item = CreateTestItem();
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(InitialTestSellIn - 1, item.SellIn);
            Assert.Equal(InitialTestQuantity - 1, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(InitialTestSellIn - 2, item.SellIn);
            Assert.Equal(InitialTestQuantity - 2, item.Quality);
        }

        [Fact]
        public void TestQualityDegradesTwiceAsFastWhenSellByDateHasPassed()
        {
            Item item = CreateTestItem();
            item.SellIn = 1;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(0, item.SellIn);
            Assert.Equal(InitialTestQuantity - 1, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(-1, item.SellIn);
            Assert.Equal(InitialTestQuantity - 3, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(-2, item.SellIn);
            Assert.Equal(InitialTestQuantity - 5, item.Quality);
        }

        [Fact]
        public void TestQualityIsNeverNegative()
        {
            Item item = CreateTestItem();
            item.SellIn = 0;
            item.Quality = 1;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(0, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void TestAgedBrieIncreasesAsItGetsOlder()
        {
            Item item = CreateTestItem(() => new BrieItem());
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity + 1, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity + 2, item.Quality);
        }

        [Fact]
        public void TestQualityIsNeverMoreThan50()
        {
            Item item = CreateTestItem(() => new BrieItem());
            item.Quality = 49;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(50, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void TestSulfurasHeverHasToBeSold()
        {
            Item item = CreateTestItem(() => new SulfurasItem());
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(InitialTestSellIn, item.SellIn);
            Program.UpdateQuality(items);
            Assert.Equal(InitialTestSellIn, item.SellIn);
        }

        [Fact]
        public void TestSulfurasNeverDecreasesInQuality()
        {
            Item item = CreateTestItem(() => new SulfurasItem());
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity, item.Quality);
        }

        [Fact]
        public void TestSulfurasQualityNeverChanges()
        {
            Item item = CreateTestItem(() => new SulfurasItem());
            int initialQuality = item.Quality;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(initialQuality, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(initialQuality, item.Quality);
        }

        [Fact]
        public void TestBackStagePassesQualityIncreasesBy2WhenThereAre10DaysOrLess()
        {
            Item item = CreateTestItem(() => new BackstagePassItem());
            item.SellIn = 11;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(10, item.SellIn);
            Assert.Equal(InitialTestQuantity + 1, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(9, item.SellIn);
            Assert.Equal(InitialTestQuantity + 3, item.Quality);
        }

        [Fact]
        public void TestBackStagePassesQualityIncreasesBy3WhenThereAre5DaysOrLess()
        {
            Item item = CreateTestItem(() => new BackstagePassItem());
            item.SellIn = 6;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(5, item.SellIn);
            Assert.Equal(InitialTestQuantity + 2, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(4, item.SellIn);
            Assert.Equal(InitialTestQuantity + 5, item.Quality);
        }

        [Fact]
        public void TestBackStagePassesQualityIs0AfterTheConcert()
        {
            Item item = CreateTestItem(() => new BackstagePassItem());
            item.SellIn = 1;
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity + 3, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void TestConjuredItemDegradesInQualityTwiceAsFastAsNormalItem()
        {
            Item item = CreateTestItem(() => new ConjuredItem());
            List<Item> items = new List<Item> { item };

            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity - 2, item.Quality);
            Program.UpdateQuality(items);
            Assert.Equal(InitialTestQuantity - 4, item.Quality);
        }

        private Item CreateTestItem(Func<Item> createItem = null)
        {
            Item item = createItem != null ? createItem() : new Item { Name = "TestItem" };
            item.Quality = InitialTestQuantity;
            item.SellIn = InitialTestSellIn;
            return item;
        }
    }
}