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
    private List<KeyValuePair<Vector2Int, FigureData>> figuresForInit = new List<KeyValuePair<Vector2Int, FigureData>>();

    public Cell[,] Cells { get; } = new Cell[_width, _height];

    public TurnManager TurnManager { get; private set; }

    private void Awake()
    {
        TurnManager = new TurnManager(this);
        _whiteCell = Core.CurrentLocation.WhiteCell;
        _blackCell = Core.CurrentLocation.BlackCell;
        _additionalCells = Core.CurrentLocation.AdditionalCells;
        InitAllFiguresDictionary();
    }

    private void Start()
    {
        InitAllCells();

        for(int i = 0; i < figuresForInit.Count; i++)
        {
            CreateFigure(figuresForInit[i]);
        }
    }

    private void InitAllFiguresDictionary()
    {
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 0), Core.FirstPlayerData.Deck.Rook));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 0), Core.FirstPlayerData.Deck.Horse));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 0), Core.FirstPlayerData.Deck.Bishop));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 0), Core.FirstPlayerData.Deck.Queen));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 0), Core.FirstPlayerData.Deck.King));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 0), Core.FirstPlayerData.Deck.Bishop));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 0), Core.FirstPlayerData.Deck.Horse));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 0), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 1), Core.FirstPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 7), Core.SecondPlayerData.Deck.Rook));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 7), Core.SecondPlayerData.Deck.Horse));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 7), Core.SecondPlayerData.Deck.Bishop));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 7), Core.SecondPlayerData.Deck.King));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 7), Core.SecondPlayerData.Deck.Queen));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 7), Core.SecondPlayerData.Deck.Bishop));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 7), Core.SecondPlayerData.Deck.Horse));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 7), Core.SecondPlayerData.Deck.Rook));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 6), Core.SecondPlayerData.Deck.Pawn));
        figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 6), Core.SecondPlayerData.Deck.Pawn));
    }

    private void CreateFigure(KeyValuePair<Vector2Int, FigureData> figureAndPosition)
    {
        var data = figureAndPosition.Value.Clone();
        MainFieldFigure figure = Instantiate(data.MainFieldFigure, transform);
        figure.Data = data;
        if (figureAndPosition.Key.x < Cells.GetLength(0) && figureAndPosition.Key.y < Cells.GetLength(1))
            figure.MoveToAnotherCell(Cells[figureAndPosition.Key.x, figureAndPosition.Key.y]);
        else
            throw new System.Exception("Фигура не может выходить за границы поля");
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