using System.Collections.Generic;
using UnityEngine;

// Напоминание: слева снизу черная клетка, само поле 8х8
public class Field : MonoBehaviour
{
    private GameObject _whiteCell;
    private GameObject _blackCell;
    private List<GameObject> _additionalCells;
    private Cell[,] cells = new Cell[width, height];
    private MainFieldFigure _currentFigure;
    private Team _currentTurn;
    private const int width = 8;
    private const int height = 8;

    public void Awake()
    {
        _whiteCell = Core.currentLocation.WhiteCell;
        _blackCell = Core.currentLocation.BlackCell;
        _additionalCells = Core.currentLocation.AdditionalCells;
    }

    public void Start()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject cell;
                if ((x % 2 == 1 && y % 2 == 0) || (x % 2 == 0 && y % 2 == 1))
                    cell = Instantiate(_whiteCell, transform);
                else
                    cell = Instantiate(_blackCell, transform);
                var cellWidth = cell.GetComponent<RectTransform>().rect.width;
                var cellHeight = cell.GetComponent<RectTransform>().rect.height;
                cell.transform.localPosition = new Vector2((x - width / 2) * cellWidth,
                                                           (y - height / 2) * cellHeight);
                //cells[x, y] = cell.GetComponent<Cell>();
            }
        }

        // [x, y] - loop variables
        // FigureData pawnData = PlayerData.Deck.Pawn.Clone()
        // MainFieldFigure figure = Instantiate(pawnData.MainFieldFigure, transform)
        // figure.data = pawnData
        // figure.cell = cells[x, y]
    }
}