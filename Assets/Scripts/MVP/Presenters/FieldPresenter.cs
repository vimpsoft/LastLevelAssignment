using UnityEngine;

public class FieldPresenter : MonoBehaviour
{
    [SerializeField]
    private Map _map; //Часть модели
    [SerializeField]
    private FieldView _fieldView; //Ну, тут вью, это понятно

    private void Start()
    {
        //Тут в реактив МВП бы привязывать реактивно, но сейчас будем привязывать тем, что есть
        _map.OnNewMap += () => _fieldView.RecreateView(_map);
        _map.OnWallAdd += _fieldView.AddWall;
        _map.OnWallRemove += _fieldView.RemoveWall;
    }
}
