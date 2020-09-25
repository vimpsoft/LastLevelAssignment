using System;
using UnityEngine;

public class FloorView : MonoBehaviour
{
    public event Action<Vector2Int> OnCoordinateClicked;

    [SerializeField]
    private Map _map;//Тут конечно надо интерфейс IMap, но сериализуемую обертку интерфейсов для простоты я здесь не буду делать

    private void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        var plane = new Plane(Vector3.up, 0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var enter = default(float);
        if (plane.Raycast(ray, out enter))
        {
            var point = ray.origin + ray.direction * enter;
            var x = (int)point.x;
            var y = (int)point.z;
            if (x < 0 || y < 0 || x >= _map.Width || y >= _map.Height)
                return;
            Debug.Log($"Clicked on {x},{y}!");
            OnCoordinateClicked(new Vector2Int(x, y));
        }
    }
}
