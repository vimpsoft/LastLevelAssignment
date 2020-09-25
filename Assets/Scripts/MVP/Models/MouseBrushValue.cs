using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MouseBrushValue", menuName = "LastLevel/MouseBrushValue", order = 0)]
public class MouseBrushValue : ScriptableObject
{
    public event Action<MouseBrush> OnUpdate; //тут бы опять объединить это все в IObservable<Vector2Int>, но ладно уж
    public MouseBrush Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnUpdate?.Invoke(value);
            }
        }
    }
    private MouseBrush _value;
}
