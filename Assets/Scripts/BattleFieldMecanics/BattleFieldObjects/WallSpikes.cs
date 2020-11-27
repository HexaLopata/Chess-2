using System.Collections.Generic;
using UnityEngine;

public class WallSpikes : TimeLimitedObject
{
    private bool _opened = true;
    
    [SerializeField] private int _damage = 30;
    [Space]
    [SerializeField] private Sprite _whiteOpenedSkin;
    [SerializeField] private Sprite _blackOpenedSkin;

    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return figure.Data.Team != Team ? BarrierType.Impassable : BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        return BarrierType.Stopable;
    }

    public override void TakeDamage(BattleFieldFigure attacker) { }
    public override void TakeDamage(int damage) { }

    public override void Visit(BattleFieldFigure visitor) { }

    public override void Execute()
    {
        if (!_opened)
        {
            var pos = OnBoardPosition;
            var cells = BattleField.BattleFieldCells;
            Image.sprite = Team == Team.Black ? _blackOpenedSkin : _whiteOpenedSkin;
            _opened = true;
            List<BattleFieldCell> turns = new List<BattleFieldCell>();
            if (pos.x + 1 < cells.GetLength(0))
                turns.Add(cells[pos.x + 1, pos.y]);
            if (pos.y > 0)
                turns.Add(cells[pos.x, pos.y - 1]);
            if (pos.x > 0)
                turns.Add(cells[pos.x - 1, pos.y]);
            if (pos.y + 1 < cells.GetLength(1))
                turns.Add(cells[pos.x, pos.y + 1]);
            
            turns.ForEach(c => c.TakeDamage(_damage));
        }
        else
        {
            _opened = false;
            Image.sprite = Team == Team.Black ? BlackSkin : WhiteSkin;
        }
        
        base.Execute();
    }
}