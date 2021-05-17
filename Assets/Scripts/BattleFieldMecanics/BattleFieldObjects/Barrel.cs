using UnityEngine;
using UnityEngine.UI;

public class Barrel : BattleFieldObject
{
    [SerializeField] private int _hp = 3;
    [SerializeField] private int _damage = 35;

    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return figure.Data.Team != Team ? BarrierType.Impassable : BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        return figure.Data.Team != Team ? BarrierType.Stopable : BarrierType.Passable;
    }

    public override void Execute()
    {
        if (_hp <= 0)
        {
            DestroyThisBattleFieldObject();
        }
        else if (_hp == 1)
        {
            GetComponent<Image>().color = Color.red;
        }
    }

    public override void TakeDamage(BattleFieldFigure attacker)
    {
        _hp -= 1;
    }

    public override void TakeDamage(int damage) { }

    public override void Visit(BattleFieldFigure visitor) { }

    public override void DestroyThisBattleFieldObject()
    {
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, 0))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x + 1, OnBoardPosition.y].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 0, 1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x, OnBoardPosition.y + 1].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, 1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x + 1, OnBoardPosition.y + 1].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, 0))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x - 1, OnBoardPosition.y].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 0, -1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x, OnBoardPosition.y - 1].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, -1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x - 1, OnBoardPosition.y - 1].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, -1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x + 1, OnBoardPosition.y - 1].TakeDamage(_damage);
        if (_controller.BattleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, 1))
            _controller.BattleField.BattleFieldCells[OnBoardPosition.x - 1, OnBoardPosition.y + 1].TakeDamage(_damage);

        base.DestroyThisBattleFieldObject();
    }
}

