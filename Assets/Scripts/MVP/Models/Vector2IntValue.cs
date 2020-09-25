using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector2IntValue", menuName = "LastLevel/Vector2IntValue", order = 0)]
public class Vector2IntValue : ScriptableObject
{
    public event Action<Vector2Int> OnUpdate; //тут бы опять объекдинить это все в IObservable<Vector2Int>, но ладно уж
    public Vector2Int Value
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
    private Vector2Int _value;
}
