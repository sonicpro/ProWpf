namespace BooleanToValueConverterExample
{
    internal class DomainObject
    {
        public DomainObject()
        {
            ShowText = false;
        }

        public bool ShowText { get; private set; }
    }
}
