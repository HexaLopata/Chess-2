using System.Collections.Generic;
using UnityEngine;

// Напоминание: слева снизу черная клетка, само поле 8х8
public class MainField : FieldBase
{
    private readonly List<KeyValuePair<Vector2Int, FigureData>> _figuresForInit = new List<KeyValuePair<Vector2Int, FigureData>>();
    public MainFieldTurnManager MainFieldTurnManager { get; private set; }
    
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
        MainFieldTurnManager = new MainFieldTurnManager(this);
        InitAllFiguresDictionary();
    }
    
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

    private void CreateFigure(KeyValuePair<Vector2Int, FigureData> figureAndPosition)
    {
        var data = figureAndPosition.Value.Clone();
        MainFieldFigure figure = Instantiate(data.MainFieldFigurePrefub, transform);
        figure.Data = data;
        figure.Data.MainFieldFigureInstance = figure;
        if (figureAndPosition.Key.x < Cells.GetLength(0) && figureAndPosition.Key.y < Cells.GetLength(1))
        {
            figure.MoveToAnotherCell(Cells[figureAndPosition.Key.x, figureAndPosition.Key.y]);
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
    public void ActivateCells(Cell[] cells)
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