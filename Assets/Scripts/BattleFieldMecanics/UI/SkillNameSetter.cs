using UnityEngine;

[RequireComponent(typeof(Chess2Text))]
public class SkillNameSetter : MonoBehaviour
{
    [SerializeField] private bool _isForFirstPlayer;
    [SerializeField] private BattleField _field;
    private BattleFieldFigure _figure;
    private Chess2Text _text;
    private void Start()
    {
        _text = GetComponent<Chess2Text>();
        if (_isForFirstPlayer)
            _figure = _field.FirstFigure;
        else
            _figure = _field.SecondFigure;
        _text.Text = _figure.Skill.Name;
    }
}
