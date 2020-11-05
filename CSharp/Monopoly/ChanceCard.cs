namespace Monopoly
{
    public abstract class ChanceCard
    {
        protected string Text { get; set; }

        protected ChanceCard(string text)
        {
            Text = text;
        }
    }
}