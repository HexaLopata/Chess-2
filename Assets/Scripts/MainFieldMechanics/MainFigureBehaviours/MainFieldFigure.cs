using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class MainFieldFigure : MonoFigure, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        var field = GetComponentInParent<MainField>();
        field.MainFieldTurnManager.OnFigureClick(this);
    }

    public virtual Cell[] GetRelevantTurn(Cell[,] cells)
    {
        return new Cell[0];
    }
}