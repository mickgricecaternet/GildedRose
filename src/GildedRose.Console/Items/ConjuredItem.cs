namespace GildedRose.Console.Items
{
    public class ConjuredItem : Item, IComplexItem
    {
        public void UpdateQuality()
        {
            Quality -= 2;
        }
    }
}