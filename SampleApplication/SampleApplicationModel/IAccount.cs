namespace SampleApplicationModel
{
    public interface IAccount
    {
        string Name { get; }

        Money Balance { get; }

        IAccount Parent { get; }
    }
}
