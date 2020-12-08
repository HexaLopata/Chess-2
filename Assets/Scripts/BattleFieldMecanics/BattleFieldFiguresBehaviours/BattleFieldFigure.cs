using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Базовый класс для фигур боевого поля
/// </summary>
public abstract class BattleFieldFigure : MonoFigure
{
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
    public int Defence => _defence;
    public int Damage => _damage;
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
    public UnityEvent onTakeDamage;

    private int _health = -1;
    private Coroutine MoveAnimation;

    [SerializeField] private int _damage = 30;
    [SerializeField] private int _defence = 15;

    protected BattleField _battleField;
    protected BattleFieldCell[,] _battleFieldCells;
    protected List<BattleFieldCell> _turns = new List<BattleFieldCell>();

    private void Start()
    {
        _battleField = GetComponentInParent<BattleField>();
        if (_battleField != null)
            _battleFieldCells = _battleField.BattleFieldCells;
    }

    /// <summary>
    /// Возвращает доступные ходы
    /// </summary>
    /// <returns></returns>
    public virtual BattleFieldCell[] GetRelevantMoves()
    {
        var result = new BattleFieldCell[_battleFieldCells.GetLength(0) * _battleFieldCells.GetLength(1)];
        for (int x = 0; x < _battleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < _battleFieldCells.GetLength(1); y++)
            {
                result[x + (y * _battleFieldCells.GetLength(1))] = _battleFieldCells[x, y];
            }
        }

        return result;
    }

    /// <summary>
    /// Возвращает доступные для атаки ходы
    /// </summary>
    /// <returns></returns>
    public virtual BattleFieldCell[] GetRelevantAttackMoves() 
    {
        var result = new BattleFieldCell[_battleFieldCells.GetLength(0) * _battleFieldCells.GetLength(1)];
        for (int x = 0; x < _battleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < _battleFieldCells.GetLength(1); y++)
            {
                result[x + (y * _battleFieldCells.GetLength(1))] = _battleFieldCells[x, y];
            }
        }

        return result;
    }

    /// <summary>
    /// Возвращает ходы доступные для атаки ходы так, как будто фигура стоит на переданной позиции
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public BattleFieldCell[] GetRelevantAttackMovesFromCustomPosition(Vector2Int position)
    {
        var realOnBoardPosition = OnBoardPosition;
        OnBoardPosition = position;
        var result = GetRelevantAttackMoves();
        OnBoardPosition = realOnBoardPosition;
        return result;
    }

    /// <summary>
    /// Возвращает ходы так, как будто фигура стоит на переданной позиции
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public BattleFieldCell[] GetRelevantMovesFromCustomPosition(Vector2Int position)
    {
        var realOnBoardPosition = OnBoardPosition;
        OnBoardPosition = position;
        var result = GetRelevantMoves();
        OnBoardPosition = realOnBoardPosition;
        return result;
    }

    /// <summary>
    /// Совершает ход или выполняет действия умения, если оно активировано
    /// </summary>
    /// <param name="selectedCell"></param>
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

    /// <summary>
    /// Выполняет ход с анимацией
    /// </summary>
    /// <param name="selectedCell"></param>
    /// <returns></returns>
    public IEnumerator TurnWithAnimation(BattleFieldCell selectedCell)
    {
        yield return StartCoroutine(MoveToAnotherCellWithAnimation(selectedCell));
        LaunchAnAttack();
        selectedCell.BattleField.BattleController.SwitchTurn();
        MoveAnimation = null;
    }

    public override void MoveToAnotherCell(CellBase cellBase)
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

    /// <summary>
    /// Атакует все доступные фигуре клетки
    /// </summary>
    public virtual void LaunchAnAttack()
    {
        var cells = GetRelevantAttackMoves();
        foreach (var cell in cells)
        {
            cell.TakeDamage(this);
        }
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damage"></param>
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

    /// <summary>
    /// Анимация получения урона
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageAnimation()
    {
        _image.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _image.color = Color.white;
    }

    /// <summary>
    /// Вспомогательный метод для добавления клеток в список ходов, если клетка, на переданных координатах существует и объекты не запрещают ход на нее
    /// </summary>
    /// <param name="cellX">Смещение относительно координат фигры по x</param>
    /// <param name="cellY">Смещение относительно координат фигры по y</param>
    /// <param name="isAttack">Проверять ли объекты на возможность атаковать по ним</param>
    protected void CheckCellAndAdd(int cellX, int cellY, bool isAttack)
    {
        var x = OnBoardPosition.x + cellX;
        var y = OnBoardPosition.y + cellY;
        var cell = _battleFieldCells[x, y];

        if (cell != null)
        {
            if (cell.BattleFieldFigure == null || isAttack)
            {
                if (cell.BattleFieldObject == null)
                {
                    _turns.Add(cell);
                }
                else
                {
                    BarrierType barrier;
                    if (isAttack)
                        barrier = cell.BattleFieldObject.CanThisFigureToAttackThrough(this);
                    else
                        barrier = cell.BattleFieldObject.CanThisFigureToCross(this);
                    if (barrier != BarrierType.Impassable)
                        _turns.Add(cell);
                }
            }
        }
    }
}