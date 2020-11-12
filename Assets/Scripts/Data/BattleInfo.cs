using System;

public class BattleInfo
{
    public FigureData FirstFigure => _firstFigure; 
    public FigureData SecondFigure => _secondFigure;
    public FigureData Winner {
        get
        {
            return _winner;
        }
        set
        {
            _winner = value;
            BattleEnd?.Invoke();
        }
    }
    public Cell CellFightingFor => _cellFightinhFor;
    public FigureData Loser { get; set; }
    
    public event Action BattleEnd;

    private FigureData _firstFigure;
    private FigureData _secondFigure;
    private FigureData _winner;
    private Cell _cellFightinhFor;

    /// <summary>
    /// Позволяет задать всю необходимую для начала битвы информацию
    /// </summary>
    /// <param name="cellFightingFor"></param>
    /// <param name="firstFigure"></param>
    /// <param name="secondFigure"></param>
    public void SetAllInitialInfo(FigureData firstFigure, FigureData secondFigure, Cell cellFightingFor)
    {
        Clear();
        _firstFigure = firstFigure;
        _secondFigure = secondFigure;
        _cellFightinhFor = cellFightingFor;
    }
    
    /// <summary>
    /// Отчищает всю старую информацию
    /// </summary>
    private void Clear()
    {
        _firstFigure = null;
        _secondFigure = null;
        _cellFightinhFor = null;
        _winner = null;
        Loser = null;
    }
}