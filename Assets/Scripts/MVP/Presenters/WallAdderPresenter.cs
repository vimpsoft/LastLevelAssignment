using UnityEngine;

public class WallAdderPresenter : UserCommandHandlerPresenterBase
{
    [SerializeField]
    private Map _map;

    protected override void handleCommand(Vector2Int coords) => _map.AddWall(coords.x, coords.y);
}
