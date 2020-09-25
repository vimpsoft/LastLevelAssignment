using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "LastLevel/Map", order = 0)]
public class Map : ScriptableObject, IMap
{
    public event Action OnNewMap; //Вместо этих эвентов я бы IObservable лучше использовал, но не будем импортировать ничего из ассет-стора
    public event Action<Vector2Int> OnWallAdd;
    public event Action<Vector2Int> OnWallRemove;

    public Node this[int x, int y]
    {
        get => _nodes[x * Height + y];
        private set => _nodes[x * Height + y] = value;
    }

    public int Width { get; private set; }
    public int Height { get; private set; }

    [SerializeField]
    private float _treshold = 0.5f;

    private Node[] _nodes;

    public void Initialize(Texture2D texture)
    {
        Width = texture.width;
        Height = texture.height;
        _nodes = new Node[Width * Height]; //Мусорим, но это редко будет происходить, так что норм
        for (int x = 0; x < texture.width; x++)
            for (int y = 0; y < texture.height; y++)
                _nodes[x * texture.height + y] = new Node(x, y, (texture.GetPixel(x, y).r + texture.GetPixel(x, y).g + texture.GetPixel(x, y).b) / 3f > _treshold); //те что темнее - препятствия
        OnNewMap?.Invoke();
    }

    public void AddWall(int wallX, int wallY)
    {
        if (wallX < 0 || wallX >= Width || wallY < 0 || wallY >= Height)
        {
            Debug.LogWarning($"AddWall: Coords ({wallX},{wallY}) are out of bounds ({Width},{Height})!");
            return;
        }

        if (this[wallX, wallY].IsTraversable)
        {
            this[wallX, wallY] = new Node(wallX, wallY, false);
            OnWallAdd?.Invoke(new Vector2Int(wallX, wallY));
        }
    }

    public void RemoveWall(int wallX, int wallY)
    {
        if (wallX < 0 || wallX >= Width || wallY < 0 || wallY >= Height)
        {
            Debug.LogWarning($"RemoveWall: Coords ({wallX},{wallY}) are out of bounds ({Width},{Height})!");
            return;
        }

        if (!this[wallX, wallY].IsTraversable)
        {
            this[wallX, wallY] = new Node(wallX, wallY, true);
            OnWallRemove?.Invoke(new Vector2Int(wallX, wallY));
        }
    }
}