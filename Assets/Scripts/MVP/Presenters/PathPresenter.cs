using System.Collections.Generic;
using UnityEngine;

public class PathPresenter : MonoBehaviour
{
    [SerializeField]
    private Vector2IntValue _startPoint;
    [SerializeField]
    private Vector2IntValue _endPoint;
    [SerializeField]
    private Map _map;

    [SerializeField]
    private PathView _view;

    void Start()
    {
        _startPoint.OnUpdate += redoPath;
        _endPoint.OnUpdate += redoPath;
        _map.OnWallAdd += redoPath;
        _map.OnWallRemove += redoPath;

        void redoPath(Vector2Int _)
        {
            var path = PathFinder.FindPath(_map, _startPoint.Value, _endPoint.Value);
            _view.DrawPath(path);
        }
    }
}
