using System.Collections.Generic;
using UnityEngine;

internal class PathView : MonoBehaviour
{
    [SerializeField]
    private GameObject _pathPointPrefab;

    private List<GameObject> _pathPoints = new List<GameObject>();

    internal void DrawPath(IReadOnlyList<Vector2Int> path)
    {
        for (int i = 0; i < _pathPoints.Count; i++)
            Destroy(_pathPoints[i]);
        _pathPoints.Clear();

        for (int i = 0; i < path.Count; i++)
        {
            var newPathPoint = Instantiate(_pathPointPrefab);
            newPathPoint.transform.SetParent(transform);
            newPathPoint.transform.localPosition = new Vector3(path[i].x, 0, path[i].y);
            newPathPoint.SetActive(true);
            _pathPoints.Add(newPathPoint);
        }
    }
}