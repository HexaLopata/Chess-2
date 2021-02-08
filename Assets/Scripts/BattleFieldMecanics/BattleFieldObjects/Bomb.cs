using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : TimeLimitedObject
{
    [SerializeField] private int _damage = 50;
    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        return BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return BarrierType.Passable;
    }

    public override void TakeDamage(BattleFieldFigure attacker) {}

    public override void TakeDamage(int damage) {}

    public override void Visit(BattleFieldFigure visitor) {}

    public override void DestroyThisBattleFieldObject()
    {
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, 0))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x + 1, OnBoardPosition.y].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 0, 1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x, OnBoardPosition.y + 1].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, 1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x + 1, OnBoardPosition.y + 1].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, 0))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x - 1, OnBoardPosition.y].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 0, -1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x, OnBoardPosition.y - 1].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, -1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x - 1, OnBoardPosition.y - 1].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, -1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x + 1, OnBoardPosition.y - 1].TakeDamage(_damage);
        if(_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, 1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x - 1, OnBoardPosition.y + 1].TakeDamage(_damage);

        base.DestroyThisBattleFieldObject();
    }
}