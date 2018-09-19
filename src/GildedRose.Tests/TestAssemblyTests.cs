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
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(InitialTestSellIn - 1, item.SellIn);
            Assert.Equal(InitialTestQuantity - 1, item.Quality);
            program.UpdateQuality();
            Assert.Equal(InitialTestSellIn - 2, item.SellIn);
            Assert.Equal(InitialTestQuantity - 2, item.Quality);
        }

        [Fact]
        public void TestQualityDegradesTwiceAsFastWhenSellByDateHasPassed()
        {
            Item item = CreateTestItem();
            item.SellIn = 1;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(0, item.SellIn);
            Assert.Equal(InitialTestQuantity - 1, item.Quality);
            program.UpdateQuality();
            Assert.Equal(-1, item.SellIn);
            Assert.Equal(InitialTestQuantity - 3, item.Quality);
            program.UpdateQuality();
            Assert.Equal(-2, item.SellIn);
            Assert.Equal(InitialTestQuantity - 5, item.Quality);
        }

        [Fact]
        public void TestQualityIsNeverNegative()
        {
            Item item = CreateTestItem();
            item.SellIn = 0;
            item.Quality = 1;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(0, item.Quality);
            program.UpdateQuality();
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void TestAgedBrieIncreasesAsItGetsOlder()
        {
            Item item = CreateTestItem(() => new BrieItem());
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(InitialTestQuantity + 1, item.Quality);
            program.UpdateQuality();
            Assert.Equal(InitialTestQuantity + 2, item.Quality);
        }

        [Fact]
        public void TestQualityIsNeverMoreThan50()
        {
            Item item = CreateTestItem(() => new BrieItem());
            item.Quality = 49;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(50, item.Quality);
            program.UpdateQuality();
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void TestSulfurasHeverHasToBeSold()
        {
            Item item = CreateTestItem(() => new SulfurasItem());
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(InitialTestSellIn, item.SellIn);
            program.UpdateQuality();
            Assert.Equal(InitialTestSellIn, item.SellIn);
        }

        [Fact]
        public void TestSulfurasNeverDecreasesInQuality()
        {
            Item item = CreateTestItem(() => new SulfurasItem());
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(InitialTestQuantity, item.Quality);
            program.UpdateQuality();
            Assert.Equal(InitialTestQuantity, item.Quality);
        }

        [Fact]
        public void TestSulfurasQualityNeverChanges()
        {
            Item item = CreateTestItem(() => new SulfurasItem());
            int initialQuality = item.Quality;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(initialQuality, item.Quality);
            program.UpdateQuality();
            Assert.Equal(initialQuality, item.Quality);
        }

        [Fact]
        public void TestBackStagePassesQualityIncreasesBy2WhenThereAre10DaysOrLess()
        {
            Item item = CreateTestItem(() => new BackstagePassItem());
            item.SellIn = 11;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(10, item.SellIn);
            Assert.Equal(InitialTestQuantity + 1, item.Quality);
            program.UpdateQuality();
            Assert.Equal(9, item.SellIn);
            Assert.Equal(InitialTestQuantity + 3, item.Quality);
        }

        [Fact]
        public void TestBackStagePassesQualityIncreasesBy3WhenThereAre5DaysOrLess()
        {
            Item item = CreateTestItem(() => new BackstagePassItem());
            item.SellIn = 6;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(5, item.SellIn);
            Assert.Equal(InitialTestQuantity + 2, item.Quality);
            program.UpdateQuality();
            Assert.Equal(4, item.SellIn);
            Assert.Equal(InitialTestQuantity + 5, item.Quality);
        }

        [Fact]
        public void TestBackStagePassesQualityIs0AfterTheConcert()
        {
            Item item = CreateTestItem(() => new BackstagePassItem());
            item.SellIn = 1;
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(InitialTestQuantity + 3, item.Quality);
            program.UpdateQuality();
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void TestConjuredItemDegradesInQualityTwiceAsFastAsNormalItem()
        {
            Item item = CreateTestItem(() => new ConjuredItem());
            var program = Program.CreateProgram(new List<Item> { item });

            program.UpdateQuality();
            Assert.Equal(InitialTestQuantity - 2, item.Quality);
            program.UpdateQuality();
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