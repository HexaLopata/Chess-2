public class AntiMagic : Talent
{
    protected override void TalentAction()
    {
        var enemy = _controller.CurrentFigure;
        enemy.Skill.Delay = enemy.Skill.MaxDelay;
    }
}