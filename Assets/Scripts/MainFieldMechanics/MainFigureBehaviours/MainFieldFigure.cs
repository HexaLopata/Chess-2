using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class MainFieldFigure : MonoFigure, IPointerClickHandler
{
    public string FigureName => _figureName;
    
    [SerializeField] private string _figureName;
    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        var field = GetComponentInParent<MainField>();
        field.MainFieldTurnManager.OnFigureClick(this);
    }

    public virtual CellBase[] GetRelevantTurn(CellBase[,] cells)
    {
        return new CellBase[0];
    }
}