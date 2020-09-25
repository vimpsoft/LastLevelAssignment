using UnityEngine;

public class StartAndFinishPointsPresenter : UserCommandHandlerPresenterBase
{
    [SerializeField, Tooltip("Подставьте нужную вьюху")]
    private Transform _view;

    protected override void handleCommand(Vector2Int coords) => _view.position = new Vector3(coords.x, 0, coords.y);
}
