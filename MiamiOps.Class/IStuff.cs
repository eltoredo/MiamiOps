namespace MiamiOps
{
    public interface IStuff
    {
        void WalkOn();

        string Name { get; }
        Vector Position { get; }
    }

    public interface IStuffFactory
    {
        IStuff Create();
    }
}
