using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireSoul : TimeLimitedObject
{
    [SerializeField] private int _damage = 30;
    
    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        return BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return BarrierType.Impassable;
    }

    public override void Execute()
    {
        base.Execute();
        if (BattleField.BattleController.CurrentTurn != Team)
        {
            if (_turnRemains > 0)
            {
                StartCoroutine(Turn());
            }
        }
    }

    private IEnumerator Turn()
    {
        Debug.Log("Turn");
        while(_controller != null && _controller.IsAnimationPlaying)
        {
            yield return new WaitForSeconds(BattleController.animationDelay);  
            Debug.Log("Delay");
        }

        var cells = GetRelevantTurns(false);
        if(cells.Length != 0)
        {
            var newCell = cells[Random.Range(0, cells.Length)];
            yield return MoveToAnotherCellWithAnimation(newCell);
            var attackTurns = GetRelevantTurns(true);
            foreach (var cell in attackTurns)
            {
                if(cell.Figure != null && cell.Figure.Team != Team)
                cell.TakeDamage(_damage);
            }
        }
    }

    private BattleFieldCell[] GetRelevantTurns(bool isAttack)
    {
        var turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;

        if (BattleField.BattleFieldCells.GetLength(1) > y + 1 && BattleField.BattleFieldCells.GetLength(0) > x + 1)
            CheckCellAndAdd(1, 1, ref turns, isAttack);
        if (BattleField.BattleFieldCells.GetLength(1) > y + 1 && x > 0)
            CheckCellAndAdd(-1, 1, ref turns, isAttack);
        if (y > 0 && BattleField.BattleFieldCells.GetLength(0) > x + 1)
            CheckCellAndAdd(1, -1, ref turns, isAttack);
        if (y > 0 && x > 0)
            CheckCellAndAdd(-1, -1, ref turns, isAttack);

        return turns.ToArray();
    }

    protected void CheckCellAndAdd(int cellX, int cellY, ref List<BattleFieldCell> turns, bool isAttack)
    {
        var x = OnBoardPosition.x + cellX;
        var y = OnBoardPosition.y + cellY;
        var cell = BattleField.BattleFieldCells[x, y];

        if (cell != null)
        {
            if (cell.BattleFieldFigure == null || isAttack)
            {
                if(cell.BattleFieldObject == null || !(cell.BattleFieldObject is SquireSoul))
                    turns.Add(cell);
            }
        }
    }


    public override void TakeDamage(BattleFieldFigure attacker) {}

    public override void TakeDamage(int damage) {}

    public override void Visit(BattleFieldFigure visitor) {}
}