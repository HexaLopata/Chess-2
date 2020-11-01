using UnityEngine;

public class Cell : MonoBehaviour
{
    private MainFieldFigure _currentFigure;
    private CellState state = CellState.NotActive;

    // OnClick {
    // field = GetParentComponent<Field>()
    // if state == active && field.currentFigure != null
    // {
    //      if currentFigure != null
    //      {
    //          field.currentFigure.cell.figure = null
    //          field.currentFigure.cell = this
    //          ... Обновление информации о бое в Core и переход на сцену битвы
    //          ... Берем информацию о битве и ставим победителя на эту клетку
    //          currentFigure = [winner]
    //          [winner].data.
    //          field.currentFigure = null;
    //          
    //      }
    //      else
    //      {
    //          
    //      
    //
}

public enum CellState
{
    NotActive,
    Active,
}