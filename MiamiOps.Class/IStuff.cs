namespace MiamiOps
{
    public interface IStuff
    {
        void WalkOn(IStuff stuff, Round ctx);

        string Name { get; }
        Vector Position { get; }
    }

    public interface IStuffFactory
    {
        IStuff Create();
    }
}
