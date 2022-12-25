namespace Model
{
    public class Port
    {
        public bool? IsOpen { get; }

        public int Number { get; }

        public Port(int number, bool? isOpen)
        {
            IsOpen = isOpen;
            Number = number;
        }
    }
}
