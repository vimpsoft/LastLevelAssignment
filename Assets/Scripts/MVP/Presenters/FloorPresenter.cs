using UnityEngine;

public class FloorPresenter : MonoBehaviour
{
    [SerializeField]
    private FloorView _view;
    [SerializeField]
    private Vector2IntValue _floorClicks;

    private void Start()
    {
        _view.OnCoordinateClicked += value => _floorClicks.Value = value;
    }
}
