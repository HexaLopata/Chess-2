using System;

public class BattleInfo
{
    public FigureData FirstFigure => _firstFigure; 
    public FigureData SecondFigure => _secondFigure;
    public FigureData Winner 
    {
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
    public CellBase CellBaseFightingFor => _cellFightingFor;
    public FigureData Loser { get; set; }
    
    public event Action BattleEnd;

    private FigureData _firstFigure;
    private FigureData _secondFigure;
    private FigureData _winner;
    private CellBase _cellFightingFor;

    /// <summary>
    /// Позволяет задать всю необходимую для начала битвы информацию
    /// </summary>
    /// <param name="cellBaseFightingFor"></param>
    /// <param name="firstFigure"></param>
    /// <param name="secondFigure"></param>
    public void SetAllInitialInfo(FigureData firstFigure, FigureData secondFigure, CellBase cellBaseFightingFor)
    {
        Clear();
        _firstFigure = firstFigure;
        _secondFigure = secondFigure;
        _cellFightingFor = cellBaseFightingFor;
    }
    
    /// <summary>
    /// Отчищает всю старую информацию
    /// </summary>
    private void Clear()
    {
        _firstFigure = null;
        _secondFigure = null;
        _cellFightingFor = null;
        _winner = null;
        Loser = null;
    }
}