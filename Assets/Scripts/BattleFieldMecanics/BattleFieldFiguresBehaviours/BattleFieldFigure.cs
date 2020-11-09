using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BattleFieldFigure : MonoFigure
{
    protected BattleField _battleField;
    public int Health { get; set; } = -1;
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Class: {GetType().Name}, Team: {Data.Team}, Health: {Data.Health}");
    }

    private void Start()
    {
        _battleField = GetComponentInParent<BattleField>();
        SetDamageAndDefence();
    }

    protected abstract void SetDamageAndDefence();

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageAnimation());
        Health -= (damage - Defence);
        Data.Health = Health;
        if (Health <= 0)
        {
            if(Data.Team == Team.Black)
                _battleField.BattleController.SetBattleResult(Team.White);
            else
                _battleField.BattleController.SetBattleResult(Team.Black);
        }
    }

    private IEnumerator DamageAnimation()
    {
        _image.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _image.color = Color.white;
    }
}
