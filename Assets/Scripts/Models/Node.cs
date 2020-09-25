public struct Node
{
    public readonly int X;
    public readonly int Y;
    public readonly bool IsTraversable;

    public Node(int i, int j, bool isTraversable)
    {
        X = i;
        Y = j;
        IsTraversable = isTraversable;
    }
}