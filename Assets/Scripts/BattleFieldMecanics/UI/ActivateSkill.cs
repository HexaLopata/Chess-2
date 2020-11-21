using UnityEngine;
using UnityEngine.UI;

public class ActivateSkill : MonoBehaviour
{
    [SerializeField] private BattleField _field;
    [SerializeField] private bool IsForFirstPlayer; // Если true, то кнопка применяется для первого игрока, иначе для второго
    private BattleFieldFigure _figure;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(Activate);
        if (IsForFirstPlayer)
            _figure = _field.FirstFigure;
        else
            _figure = _field.SecondFigure;
        if(_figure.Skill != null)
            _figure.Skill.activateOrDisactivate.AddListener(UpdateButton);
    }

    private void Activate()
    {
        if (_field.BattleController.CurrentTurn == _figure.Data.Team)
        {
            if (_figure.Skill != null)
                _figure.Skill.Activate(_figure);
        }
    }

    private void UpdateButton()
    {
        if(_figure.Skill.IsActive)
            _image.color = Color.red;
        else
            _image.color = Color.white;
    }
}
