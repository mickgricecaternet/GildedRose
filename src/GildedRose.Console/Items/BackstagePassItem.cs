namespace GildedRose.Console.Items
{
    public class BackstagePassItem : Item, IComplexItem
    {
        public void UpdateQuality()
        {
            if (Quality < Program.MaxQuality)
            {
                if (SellIn <= 0)
                {
                    Quality = 0;
                }
                else if (SellIn < 6)
                {
                    Quality += 3;
                }
                else if (SellIn < 11)
                {
                    Quality += 2;
                }
                else
                {
                    Quality++;
                }
            }
        }
    }
}