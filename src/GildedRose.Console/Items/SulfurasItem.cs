namespace GildedRose.Console.Items
{
    public class SulfurasItem : Item, IComplexItem
    {
        public SulfurasItem()
        {
            Quality = 80;
        }

        public void UpdateQuality()
        {
            SellIn++;   // Counter the default SellIn
        }
    }
}