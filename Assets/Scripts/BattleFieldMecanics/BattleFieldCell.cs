using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleFieldCell : CellBase
{
    #region public Properties

    public BattleFieldObject BattleFieldObject { get; set; }
    public BattleField BattleField => _battleField;
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

    #endregion

    #region private Fields

    private BattleField _battleField;
    private Coroutine _damageAnimation;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if(_field is BattleField)
            _battleField = (BattleField)_field;
        else
        {
            throw new Exception("Клетка типа BattleFieldCell должна быть дочереней от поля класса BattleField или его подклассов");
        }
    }

    #endregion

    #region public Methods

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (State == CellState.Active)
        {
            var currentFigure = _battleField.BattleController.CurrentFigure;
            currentFigure.Turn(this);
        }
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

        if (BattleFieldObject != null)
        {
            if (BattleFieldObject.Team != attacker.Data.Team)
            {
                BattleFieldObject.TakeDamage(attacker);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _damageAnimation = StartCoroutine(DamageAnimation());
        if (BattleFieldFigure != null)
        {
            BattleFieldFigure.TakeDamage(damage);
        }

        if (BattleFieldObject != null)
        {
            BattleFieldObject.TakeDamage(damage);
        }
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

    #endregion

    #region private Methods

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

    #endregion
}