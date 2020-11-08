using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleFieldCell : Cell
{
    private BattleField _battleField;
    private Coroutine _damageAnimation;
    public BattleFieldObject BattleFieldObject { get; set; }

    public override FigureData Figure
    {
        get => base.Figure;
        set
        {
            if (value != null)
            {
                BattleFieldFigure = value.BattleFieldFigureInstance;
            }
            else
            {
                BattleFieldFigure = null;
            }
            
            base.Figure = value;
        }
    }
    public BattleFieldFigure BattleFieldFigure { get; set; }

    private void Start()
    {
        if(_field is BattleField)
            _battleField = (BattleField)_field;
        else
        {
            throw new Exception("Клетка типа BattleFieldCell должна быть дочереней от поля класса BattleField или его подклассов");
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(OnBoardPosition);
        _battleField.BattleController.OnCellClick(this);
    }

    /// <summary>
    /// Возникает, если эта ячейка находится в радиусе поражения фигуры во время атаки
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(BattleFieldFigure attacker)
    {
        _damageAnimation = StartCoroutine(DamageAnimation());
        if (BattleFieldFigure != null)
        {
            if(BattleFieldFigure.Data.Team != attacker.Data.Team)
                BattleFieldFigure.TakeDamage(attacker.Damage);
        }
    }

    private IEnumerator DamageAnimation()
    {
        _image.sprite = _enemy;
        yield return new WaitForSeconds(0.2f);
        if(State == CellState.Active)
            _image.sprite = _active;
        else
            _image.sprite = _normal;
        _damageAnimation = null;
    }
    
    public override void Activate()
    {
        State = CellState.Active;
        if (_damageAnimation == null)
        {
            _image.sprite = _active;
        }
    }

    public override void Deactivate()
    {
        State = CellState.NotActive;
        if (_damageAnimation == null)
        {
            _image.sprite = _normal;
        }
    }
}