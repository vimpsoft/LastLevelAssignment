using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallPrefab;
    [SerializeField]
    private GameObject _floor;

    private Dictionary<Vector2Int, GameObject> _walls = new Dictionary<Vector2Int, GameObject>();

    public void RecreateView(IMap map)
    {
        //Тут надо бы пул
        foreach (var (_, go) in _walls)
            Destroy(go);
        _walls.Clear();

        for (int x = 0; x < map.Width; x++)
            for (int y = 0; y < map.Height; y++)
                if (!map[x, y].IsTraversable)
                    _walls.Add(new Vector2Int(x, y), createWallAt(x, y));
        _floor.transform.localPosition = new Vector3(map.Width / 2f, 0, map.Height / 2);
        _floor.transform.localScale = new Vector3(map.Width, 1, map.Height);
        _floor.SetActive(true);
    }

    private GameObject createWallAt(int x, int y)
    {
        var result = Instantiate(_wallPrefab);
        result.transform.localPosition = new Vector3(x, 0, y);
        result.SetActive(true);
        result.transform.SetParent(transform);
        return result;
    }

    internal void AddWall(Vector2Int coords)
    {
        if (_walls.ContainsKey(coords))
        {
            Debug.LogWarning($"AddWall when it's already there");
            return;
        }
        _walls.Add(coords, createWallAt(coords.x, coords.y));
    }

    internal void RemoveWall(Vector2Int coords)
    {
        if (!_walls.ContainsKey(coords))
        {
            Debug.LogWarning($"RemoveWall when there's none");
            return;
        }
        Destroy(_walls[coords]);
        _walls.Remove(coords);
    }
}
