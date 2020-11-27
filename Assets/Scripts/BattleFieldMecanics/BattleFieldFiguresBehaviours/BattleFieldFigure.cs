using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BattleFieldFigure : MonoFigure
{
    #region public Properties
    
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (Data != null)
            {
                Data.Health = value;
                if (Health <= 0 && Data.MainFieldFigureInstance != null)
                {
                    Data.MainFieldFigureInstance.DestroyThisFigure();
                    DestroyThisFigure();
                }
            }
        }
    }
    public int Defence { get; set; } = 0;
    public int Damage { get; protected set; } = 0;
    public override FigureData Data
    {
        get
        {
            return _data;
        }
        set
        {
            Health = value.Health;
            value.BattleFieldFigureInstance = this;
            base.Data = value;
        } 
    }
    public BattleField BattleField => _battleField;
    public Skill Skill { get; set; }

    #endregion

    #region private Fields
    
    private int _health = -1;
    private Coroutine MoveAnimation;

    #endregion

    #region protected Fields

    protected BattleField _battleField;

    #endregion

    #region Events

    public UnityEvent onTakeDamage;

    #endregion

    #region public Methods

    public virtual BattleFieldCell[] GetRelevantMoves(BattleFieldCell[,] battleFieldCells)
    {
        var result = new BattleFieldCell[battleFieldCells.GetLength(0) * battleFieldCells.GetLength(1)];
        for (int x = 0; x < battleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < battleFieldCells.GetLength(1); y++)
            {
                result[x + (y * battleFieldCells.GetLength(1))] = battleFieldCells[x, y];
            }
        }

        return result;
    }
    public virtual BattleFieldCell[] GetRelevantAttackMoves(BattleFieldCell[,] battleFieldCells) 
    {
        var result = new BattleFieldCell[battleFieldCells.GetLength(0) * battleFieldCells.GetLength(1)];
        for (int x = 0; x < battleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < battleFieldCells.GetLength(1); y++)
            {
                result[x + (y * battleFieldCells.GetLength(1))] = battleFieldCells[x, y];
            }
        }

        return result;
    }
    public virtual void Turn(BattleFieldCell selectedCell)
    {
        if (MoveAnimation == null)
        {
            if (Skill != null && Skill.IsActive)
            {
                Skill.IsActive = false;
                Skill.Execute(this, selectedCell);
            }
            else
            {
                if (selectedCell != null)
                {
                    MoveAnimation = StartCoroutine(TurnWithAnimation(selectedCell));
                }
            }
        }
    }

    private IEnumerator TurnWithAnimation(BattleFieldCell selectedCell)
    {
        yield return StartCoroutine(MoveToAnotherCellWithAnimation(selectedCell));
        LaunchAnAttack();
        selectedCell.BattleField.BattleController.SwitchTurn();
        MoveAnimation = null;
    }
    
    protected override void MoveToAnotherCell(CellBase cellBase)
    {
        if (cellBase is BattleFieldCell)
        {
            var battleCell = cellBase as BattleFieldCell;
            
            if(battleCell.BattleFieldObject == null)
            {
                base.MoveToAnotherCell(cellBase);
            }
            else
            {
                if (battleCell.BattleFieldObject.CanThisFigureToCross(this) != BarrierType.Impassable)
                {
                    battleCell.BattleFieldObject.Visit(this);
                    base.MoveToAnotherCell(cellBase);
                }
                else
                    throw new Exception("Фигура пытается переместиться на клетку, на которую не должна");
            }
        }
        else
            throw new Exception("Фигура этого типа должна быть перемещена на клетку типа BattleFieldCell");
    }
    public virtual void LaunchAnAttack()
    {
        var cells = GetRelevantAttackMoves(_battleField.BattleFieldCells);
        foreach (var cell in cells)
        {
            cell.TakeDamage(this);
        }
    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageAnimation());
        Health -= (damage - Defence);
        onTakeDamage.Invoke();
        if (Health <= 0)
        {
            if(Data.Team == Team.Black)
                _battleField.BattleController.SetBattleResult(Team.White);
            else
                _battleField.BattleController.SetBattleResult(Team.Black);
        }
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        _battleField = GetComponentInParent<BattleField>();
        SetDamageAndDefence();
    }

    #endregion

    #region private Methods

    private IEnumerator DamageAnimation()
    {
        _image.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _image.color = Color.white;
    }

    #endregion

    #region protected Methods

    protected abstract void SetDamageAndDefence();

    #endregion
}