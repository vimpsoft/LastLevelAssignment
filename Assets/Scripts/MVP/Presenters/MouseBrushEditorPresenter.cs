using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// У этого презентера нет отдельного скрипта вьюхи - это нормально. Его модель - это MouseBrushValue, и он, как и все презентеры, связывает модель с вью (в данном случае с голыми компонентами UI).
/// Лучше конечно реактивно привязывать, но так тоже сойдет
/// </summary>
public class MouseBrushEditorPresenter : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _finishButton;
    [SerializeField]
    private Button _addWallButton;
    [SerializeField]
    private Button _removeWallButton;
    [SerializeField]
    private Text _brushLabel;

    [SerializeField]
    private MouseBrushValue _mouseBrush;

    void Start()
    {
        _startButton.onClick.AddListener(() => _mouseBrush.Value = MouseBrush.Start);
        _finishButton.onClick.AddListener(() => _mouseBrush.Value = MouseBrush.Finish);
        _addWallButton.onClick.AddListener(() => _mouseBrush.Value = MouseBrush.AddWall);
        _removeWallButton.onClick.AddListener(() => _mouseBrush.Value = MouseBrush.RemoveWall);
        _mouseBrush.OnUpdate += value => _brushLabel.text = getLabelText(value);

        //А вот этих 2 строк здесь бы не было если бы мы привязывали реактивно
        _brushLabel.text = getLabelText(_mouseBrush.Value); //Нам надо задать изначальное значение
        string getLabelText(MouseBrush brush) => $"Current Brush: {brush}";
    }
}
