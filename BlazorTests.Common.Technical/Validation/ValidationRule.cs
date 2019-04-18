namespace BlazorTests.Common.Technical.Validation
{
    public abstract class ValidationRule
    {
        public string PropertyName { get; set; }
        public abstract string Check();
    }
}
