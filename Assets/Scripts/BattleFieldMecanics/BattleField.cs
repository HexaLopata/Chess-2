using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleField : FieldBase
{
    public BattleController BattleController => _battleController;
    public BattleFieldFigure FirstFigure { get; private set; }
    public BattleFieldFigure SecondFigure { get; private set; }
    public BattleFieldCell[,] BattleFieldCells { get; private set; }

    private BattleInfo _battleInfo;
    [SerializeField] private BattleController _battleController;
    protected override void AdditionalAwakeInit()
    {
        _whiteCell = Core.CurrentLocation.BattleFieldWhiteCell;
        _blackCell = Core.CurrentLocation.BattleFieldBlackCell;
        _additionalCells = new List<GameObject>();
        Width = Random.Range(4, 7);
        Height = Random.Range(4, 7);
    }

    protected override void AdditionalStartInit()
    {
        _battleInfo = Core.BattleInfo;
        Team firstTurn;
        // Сделать сцену активной пришлось именно здесь, иначе возникал ArgumentException - invalid scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
        if (_battleInfo.FirstFigure.Team == Team.White)
        {
            FirstFigure = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub, transform);
            SecondFigure = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub, transform);
            FirstFigure.Data = _battleInfo.FirstFigure;
            SecondFigure.Data = _battleInfo.SecondFigure;
            firstTurn = Team.White;
        }
        else
        {
            SecondFigure = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub, transform);
            FirstFigure = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub, transform);
            SecondFigure.Data = _battleInfo.FirstFigure;
            FirstFigure.Data = _battleInfo.SecondFigure;
            firstTurn = Team.Black;
        }
        FirstFigure.MoveToAnotherCell(Cells[Width - 1, 0]);
        SecondFigure.MoveToAnotherCell(Cells[0, Height - 1]);
      
        
        BattleFieldCells = new BattleFieldCell[Cells.GetLength(0), Cells.GetLength(1)];
        for (int x = 0; x < BattleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < BattleFieldCells.GetLength(1); y++)
            {
                BattleFieldCells[x, y] = (BattleFieldCell)Cells[x, y];
            }
        }
        _battleController.SwitchTurn(firstTurn);
    }
    
    public void ActivateAllCells(BattleFieldCell[] battleFieldCells)
    {
        DeactivateAllCells();
        foreach (var cell in battleFieldCells)
        {
            cell.Activate();
        }
    }

    private void DeactivateAllCells()
    {
        if (BattleFieldCells != null)
        {
            foreach (var cell in BattleFieldCells)
            {
                cell.Deactivate();
            }
        }
    }
}
