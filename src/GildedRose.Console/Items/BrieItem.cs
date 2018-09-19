namespace GildedRose.Console.Items
{
    public class BrieItem : Item, IComplexItem
    {
        public void UpdateQuality()
        {
            if (Quality < Program.MaxQuality)
            {
                Quality++;
            }
        }
    }
}