using UnityEngine;

public class WallRemoverPresenter : UserCommandHandlerPresenterBase
{
    [SerializeField]
    private Map _map;

    protected override void handleCommand(Vector2Int coords) => _map.RemoveWall(coords.x, coords.y);
}
