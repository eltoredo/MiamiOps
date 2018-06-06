namespace MiamiOps
{
    public interface IStuff
    {
        void WalkOn(Round ctx);

        string Name { get; }
        Vector Position { get; }
    }

    public interface IStuffFactory
    {
        IStuff Create();
    }
}
