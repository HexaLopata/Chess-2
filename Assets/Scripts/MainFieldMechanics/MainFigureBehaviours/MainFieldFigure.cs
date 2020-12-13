using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Абстрактный класс для фигур главного поля
/// </summary>
[RequireComponent(typeof(RectTransform), typeof(Image))]
public class MainFieldFigure : MonoFigure, IPointerClickHandler
{
    public string FigureName => _figureName;
    public MainField Field => _field;

    [SerializeField] private string _figureName;

    protected List<CellBase> _turns = new List<CellBase>();
    protected MainField _field;
    protected CellBase[,] _cells;

    private void Start()
    {
        _field = GetComponentInParent<MainField>();
        if (_field != null)
            _cells = _field.Cells;
    }

    /// <summary>
    /// Оповещает менеджера ходов о клике на эту фигуру
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        _field.MainFieldTurnManager.OnFigureClick(this);
    }

    /// <summary>
    /// Возвращает доступные для перемещения клетки
    /// </summary>
    /// <param name="cells"></param>
    /// <returns></returns>
    public virtual CellBase[] GetRelevantTurn(CellBase[,] cells)
    {
        return new CellBase[0];
    }

    /// <summary>
    /// Заполняет доступные ходы всеми возможными клетками в заданном направлении
    /// </summary>
    /// <param name="cellX"></param>
    /// <param name="cellY"></param>
    protected void GetEndlessDistanceMoves(int cellX, int cellY)
    {
        var localX = OnBoardPosition.x;
        var localY = OnBoardPosition.y;

        while (IsCellExists(cellX, cellY, localX, localY))
        {
            localX += cellX;
            localY += cellY;
            var cell = _cells[localX, localY];
            _turns.Add(cell);
            if (cell.Figure != null)
                break;
        }
    }

    /// <summary>
    /// Вспомогательный метод для определения, существует ли клетка в заданном направлении
    /// </summary>
    /// <param name="cellX">Текущая условная координата x</param>
    /// <param name="cellY">Текущая условная координата y</param>
    /// <param name="localX">Координата x на которую будет происходить смещение</param>
    /// <param name="localY">Координата y на которую будет происходить смещение</param>
    /// <returns></returns>
    private bool IsCellExists(int cellX, int cellY, int localX, int localY)
    {
        bool result;
        if (cellX > 0)
            result = _cells.GetLength(0) > localX + cellX;
        else if (cellX < 0)
            result = localX > 0;
        else
            result = true;
        if (cellY > 0)
            result = result && _cells.GetLength(1) > localY + cellY;
        else if (cellY < 0)
            result = result && localY > 0;
        return result;
    }
}