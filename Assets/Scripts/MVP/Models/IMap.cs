using System;
using UnityEngine;

/// <summary>
/// С помощью интерфейса отделяем то, что можно отдать вьюхе от внутренней бизнес-логики
/// </summary>
public interface IMap
{
    event Action<Vector2Int> OnWallAdd;
    event Action<Vector2Int> OnWallRemove;
    int Width { get; }
    int Height { get; }
    Node this[int x, int y] { get; }
}