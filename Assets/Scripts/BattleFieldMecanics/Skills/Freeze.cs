using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : Skill
{
    [SerializeField] private float _freezeTime = 3;
    [SerializeField] private Image _iceImage;
    
    private Image _iceImageInstance;
    private float _currentFreezeTime = 0;
    private bool _freezing;

    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        base.Execute(figure, cell);

        // Если противник заморожен, то умение будет активно еще ход
        if(_freezing)
            IsActive = true;

        // Если откат прошел и умение еще не активно
        if (_delay <= 0 && !IsActive)
        {
            var attackedCells = figure.GetRelevantAttackMoves(figure.BattleField.BattleFieldCells);
            foreach (var attackedCell in attackedCells)
            {
                // Если попали в противника, то указывает это в переменную _freezing и повторно активирует умение
                if (attackedCell.BattleFieldFigure != null)
                {
                    _freezing = true;
                    IsActive = true;
                    _iceImageInstance = Instantiate(_iceImage, figure.BattleField.transform);
                    _iceImageInstance.rectTransform.localPosition = attackedCell.RectTransform.localPosition;
                }
                attackedCell.TakeDamage(0);
            }
            _delay = _maxDelay;
        }

        // Если противник еще заморожен, то ходим на выбранную клетку обычным образом
        if (_freezing && cell != null)
        {
            StartCoroutine(FigureTurn(figure, cell));
            _currentFreezeTime++;
        }
    }

    private IEnumerator FigureTurn(BattleFieldFigure figure, CellBase cell)
    {
        yield return figure.MoveToAnotherCellWithAnimation(cell);
        figure.LaunchAnAttack();
        _controller.BattleField.ActivateAllCells(figure.GetRelevantMoves(figure.BattleField.BattleFieldCells));
        if (_currentFreezeTime >= _freezeTime)
        {
            _currentFreezeTime = 0;
            _freezing = false;
            IsActive = false;
            Destroy(_iceImageInstance);
            _controller.SwitchTurn();
        }
    }

    public override void Activate(BattleFieldFigure figure)
    {
        IsActive = false;
        Execute(figure, null);
    }
}