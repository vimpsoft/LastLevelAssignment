using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallPrefab;
    [SerializeField]
    private GameObject _floor;

    private List<GameObject> _walls = new List<GameObject>();

    public void RecreateView(IMap map)
    {
        //Тут надо бы пул
        for (int i = 0; i < _walls.Count; i++)
            Destroy(_walls[i]);
        _walls.Clear();

        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (!map[x, y].IsTraversable)
                {
                    var newWall = Instantiate(_wallPrefab);
                    newWall.transform.localPosition = new Vector3(x, 0, y);
                    newWall.SetActive(true);
                    newWall.transform.SetParent(transform);
                    _walls.Add(newWall);
                }
            }
        }
        _floor.transform.localPosition = new Vector3(map.Width / 2f, 0, map.Height / 2);
        _floor.transform.localScale = new Vector3(map.Width, 1, map.Height);
        _floor.SetActive(true);
    }
}
