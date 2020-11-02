using System.Collections.Generic;
using UnityEngine;

// Напоминание: слева снизу черная клетка, само поле 8х8
public class Field : MonoBehaviour
{
    private GameObject _whiteCell;
    private GameObject _blackCell;
    private List<GameObject> _additionalCells;
    private const int _width = 8;
    private const int _height = 8;
    private float _cellWidth;
    private float _cellHeight;

    public Cell[,] Cells { get; } = new Cell[_width, _height];

    public TurnManager TurnManager { get; private set; }

    private void Awake()
    {
        TurnManager = new TurnManager(this);
        _whiteCell = Core.currentLocation.WhiteCell;
        _blackCell = Core.currentLocation.BlackCell;
        _additionalCells = Core.currentLocation.AdditionalCells;
    }

    private void Start()
    {
        InitAllCells();


        // ToDo Вынести положение фигур в Dictionary<FigureData, Vector2> и создать отдельный метод для расстановки
        var pawnData3 = Core.firstPlayerData.Deck.Pawn.Clone();
        MainFieldFigure pawn3 = Instantiate(pawnData3.MainFieldFigure, transform);
        pawn3.Data = pawnData3;
        pawn3.MoveToAnotherCell(Cells[0,4]);

        var pawnData = Core.firstPlayerData.Deck.Pawn.Clone();
        MainFieldFigure pawn = Instantiate(pawnData.MainFieldFigure, transform);
        pawn.Data = pawnData;
        pawn.MoveToAnotherCell(Cells[0, 1]);

        var pawnData2 = Core.secondPlayerData.Deck.Pawn.Clone();
        MainFieldFigure pawn2 = Instantiate(pawnData.MainFieldFigure, transform);
        pawn2.Data = pawnData2;
        pawn2.MoveToAnotherCell(Cells[0, 2]);
    }

    private void InitAllCells()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                GameObject cell;
                if ((x % 2 == 1 && y % 2 == 0) || (x % 2 == 0 && y % 2 == 1))
                    cell = Instantiate(_whiteCell, transform);
                else
                    cell = Instantiate(_blackCell, transform);
                if (_cellHeight == 0 || _cellWidth == 0)
                {
                    _cellWidth = cell.GetComponent<RectTransform>().rect.width;
                    _cellHeight = cell.GetComponent<RectTransform>().rect.height;
                }
                cell.transform.localPosition = new Vector2((x - _width / 2) * _cellWidth,
                                                           (y - _height / 2) * _cellHeight);
                Cells[x, y] = cell.GetComponent<Cell>();
                cell.GetComponent<Cell>().OnBoardPosition = new Vector2(x, y);
            }
        }
    }

    /// <summary>
    /// Делает все переданные клетки активными
    /// </summary>
    /// <param name="cells"></param>
    public void ActivateCells(Cell[,] cells)
    {
        DeactivateAllCells();

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y].Activate();
            }
        }
    }

    /// <summary>
    /// Деактивирует все клетки
    /// </summary>
    public void DeactivateAllCells()
    {
        for (int x = 0; x < Cells.GetLength(0); x++)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                Cells[x, y].Deactivate();
            }
        }
    }
}