using UnityEngine;

public class StartAndFinishPointsPresenter : UserCommandHandlerPresenterBase
{
    [SerializeField, Tooltip("Подставьте нужную вьюху")]
    private Transform _view;
    [SerializeField]
    private Vector2IntValue _coordinatesValue;

    protected override void handleCommand(Vector2Int coords)
    {
        _view.position = new Vector3(coords.x, 0, coords.y);
        _coordinatesValue.Value = coords;
    }
}
