using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Map", menuName = "LastLevel/Map", order = 0)]
public class Map : ScriptableObject, IMap
{
    public float _treshold = 0.5f;

    public Node this[int x, int y] => _nodes[x * Height + y];

    public int Width { get; private set; }
    public int Height { get; private set; }

    public UnityEvent OnNewMap; //Вместо этого я IObservable<Unit> лучше использовал, но не будем испортировать ничего из ассет-стора

    private Node[] _nodes;

    public void Initialize(Texture2D texture)
    {
        Width = texture.width;
        Height = texture.height;
        _nodes = new Node[Width * Height]; //Мусорим, но это редко будет происходить, так что норм
        for (int x = 0; x < texture.width; x++)
            for (int y = 0; y < texture.height; y++)
                _nodes[x * texture.height + y] = new Node(x, y, (texture.GetPixel(x, y).r + texture.GetPixel(x, y).g + texture.GetPixel(x, y).b) / 3f > _treshold); //те что темнее - препятствия
        OnNewMap.Invoke();
    }
}