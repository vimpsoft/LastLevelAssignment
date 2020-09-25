using UnityEngine;

public abstract class UserCommandHandlerPresenterBase : MonoBehaviour
{
    [SerializeField, Tooltip("Настраиваем тут, за какой браш этот презентер отвечает")]
    private MouseBrush _targetBrush;

    [SerializeField]
    private MouseBrushValue _brush;
    [SerializeField]
    private Vector2IntValue _clicks;

    private void Start()
    {
        _clicks.OnUpdate += coords =>
        {
            if (_brush.Value == _targetBrush)
                handleCommand(coords);
        };
    }

    protected abstract void handleCommand(Vector2Int coords); //Делаем абстрактным а не виртуальным, потому что я не вижу кейса в котором бы наследнику не понадобилось его реализовать.
}
