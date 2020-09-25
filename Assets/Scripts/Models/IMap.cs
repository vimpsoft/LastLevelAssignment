public interface IMap
{
    int Width { get; }
    int Height { get; }
    Node this[int x, int y] { get; }
}