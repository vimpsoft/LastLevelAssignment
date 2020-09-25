using System.Collections;
using UnityEngine;

/// <summary>
/// Т.к. у игры в целом нету вью целикового, используем этот класс только чтобы стартануть модель, и вообще завести игру.
/// </summary>
public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private Texture2D _initialMapTexture;
    [SerializeField]
    private Map _map;

    /*
     * Ждем с инициализацией кадр только потому что не используем UniRx, из-за этого надо ждать пока все подпишутся на старте на все что им нужно.
     * Если бы я использовал реактивные расширения как обычно, то в этом не былы бы нужды, т.к. там где надо я бы использовал Reactive Stateful обзерваблы. 
     * Без них либо ждать кадр с инициализацией, либо слишком много бойлерплейт кода надо.
     */
    IEnumerator Start()
    {
        yield return null;
        _map.Initialize(_initialMapTexture);
    }
}
