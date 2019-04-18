namespace BlazorTests.Common.Data.Enumerations
{
    public class EnumStringValueAttribute : System.Attribute
    {
        public string Text { get; protected set; }

        public EnumStringValueAttribute(string text)
        {
            this.Text = text;
        }
    }
}
