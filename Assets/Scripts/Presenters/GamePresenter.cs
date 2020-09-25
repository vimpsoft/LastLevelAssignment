using UnityEngine;

/// <summary>
/// Т.к. у игры в целом нету вью целикового, используем этот класс только чтобы стартануть модель
/// </summary>
public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private Texture2D _initialMapTexture;
    [SerializeField]
    private Map _map;

    void Start()
    {
        _map.Initialize(_initialMapTexture);
    }
}
