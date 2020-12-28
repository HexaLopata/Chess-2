using UnityEngine;

public class AntiMagic : Talent
{
    protected override void TalentAction()
    {
        Debug.Log(_controller.CurrentFigure.Data.Team.ToString() + " is target of antimagic");
        var enemy = _controller.CurrentFigure;
        enemy.Skill.Delay = enemy.Skill.MaxDelay;
    }
}