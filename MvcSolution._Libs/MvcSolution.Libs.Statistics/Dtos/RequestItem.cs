namespace MvcSolution.Libs.Statistics
{
    public class RequestItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public RequestItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }
    }
}
