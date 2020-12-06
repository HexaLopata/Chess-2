using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Это главное поле
/// </summary>
public class MainField : FieldBase
{
    public MainFieldTurnManager MainFieldTurnManager => _turnManager;
    public SceneTransition SceneTransition
    {
        get => sceneTransition;
    }
    public List<MonoFigure> WhiteKings => _whiteKings;
    public List<MonoFigure> BlackKings => _blackKings;
    public Camera CurrentCamera => _currentCamera;

    [SerializeField] SceneTransition sceneTransition;
    [SerializeField] private Camera _currentCamera;
    [SerializeField] private MainFieldTurnManager _turnManager; 
    private readonly List<KeyValuePair<Vector2Int, FigureData>> _figuresForInit = new List<KeyValuePair<Vector2Int, FigureData>>();
    private List<MonoFigure> _whiteKings = new List<MonoFigure>();
    private List<MonoFigure> _blackKings = new List<MonoFigure>();
    
    protected override void AdditionalStartInit()
    {
        for (int i = 0; i < _figuresForInit.Count; i++)
        {
            CreateFigure(_figuresForInit[i]);
        }
    }
    
    protected override void AdditionalAwakeInit()
    {
        _whiteCell = Core.CurrentLocation.MainFieldWhiteCell;
        _blackCell = Core.CurrentLocation.MainFieldBlackCell;
        _additionalCells = Core.CurrentLocation.AdditionalCells;
        Width = 8;
        Height = 8;
        InitAllFiguresDictionary();
    }
    
    /// <summary>
    /// Расставляет фигры на поле в соответствии правилам шахмат
    /// </summary>
    private void InitAllFiguresDictionary()
    {
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 0), Core.FirstPlayerData.Deck.Rook));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 0), Core.FirstPlayerData.Deck.Horse));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 0), Core.FirstPlayerData.Deck.Bishop));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 0), Core.FirstPlayerData.Deck.Queen));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 0), Core.FirstPlayerData.Deck.King));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 0), Core.FirstPlayerData.Deck.Bishop));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 0), Core.FirstPlayerData.Deck.Horse));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 0), Core.FirstPlayerData.Deck.Rook));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 1), Core.FirstPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 7), Core.SecondPlayerData.Deck.Rook));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 7), Core.SecondPlayerData.Deck.Horse));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 7), Core.SecondPlayerData.Deck.Bishop));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 7), Core.SecondPlayerData.Deck.Queen));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 7), Core.SecondPlayerData.Deck.King));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 7), Core.SecondPlayerData.Deck.Bishop));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 7), Core.SecondPlayerData.Deck.Horse));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 7), Core.SecondPlayerData.Deck.Rook));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(0, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(1, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(2, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(3, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(4, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(5, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(6, 6), Core.SecondPlayerData.Deck.Pawn));
        _figuresForInit.Add(new KeyValuePair<Vector2Int, FigureData>(new Vector2Int(7, 6), Core.SecondPlayerData.Deck.Pawn));
    }

    /// <summary>
    /// Создает фигуру и размещает ее на переданной клетке
    /// </summary>
    /// <param name="figureAndPosition"></param>
    private void CreateFigure(KeyValuePair<Vector2Int, FigureData> figureAndPosition)
    {
        var data = figureAndPosition.Value.Clone();
        MainFieldFigure figure = Instantiate(data.MainFieldFigurePrefub, transform);
        figure.Data = data;
        figure.Data.MainFieldFigureInstance = figure;
        if (figure.King)
        {
            if(figure.Data.Team == Team.Black)
                _blackKings.Add(figure);
            else
                _whiteKings.Add(figure);
        }
        if (figureAndPosition.Key.x < Cells.GetLength(0) && figureAndPosition.Key.y < Cells.GetLength(1))
        {
            StartCoroutine(figure.MoveToAnotherCellWithAnimation(Cells[figureAndPosition.Key.x, figureAndPosition.Key.y]));
            if(figure is MainFieldPawn)
            {
                ((MainFieldPawn)figure).IsFirstTurn = true;
            }
        }
        else
            throw new System.Exception("Фигура не может выходить за границы поля");
    }

    /// <summary>
    /// Делает все переданные клетки активными
    /// </summary>
    /// <param name="cells"></param>
    public void ActivateCells(CellBase[] cells)
    {
        DeactivateAllCells();

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x].Activate();
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