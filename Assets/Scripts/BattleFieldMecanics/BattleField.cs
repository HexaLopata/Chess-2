using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Боевое поле
/// </summary>
public class BattleField : FieldBase
{
    public BattleController BattleController => _battleController;
    public BattleFieldFigure FirstFigure { get; private set; }
    public BattleFieldFigure SecondFigure { get; private set; }
    public BattleFieldCell[,] BattleFieldCells
    {
        get => _battleFieldCells;
        set
        {
            if (value != null)
            {
                Width = value.GetLength(0);
                Height = value.GetLength(1);
                _battleFieldCells = value;
                Cells = _battleFieldCells;
            }
        }
    }

    private BattleInfo _battleInfo;
    private BattleFieldCell[,] _battleFieldCells;

    [SerializeField] private BattleController _battleController;

    protected override void AdditionalAwakeInit()
    {
        _whiteCell = Core.CurrentLocation.BattleFieldWhiteCell;
        _blackCell = Core.CurrentLocation.BattleFieldBlackCell;
        _additionalCells = new List<GameObject>();
        Width = Random.Range(4, 7);
        Height = Random.Range(4, 7);
    }

    protected override void AdditionalStartInit()
    {
        _battleInfo = Core.BattleInfo;

        // Сделать сцену активной пришлось именно здесь, иначе возникал ArgumentException - invalid scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
        if (_battleInfo.FirstFigure.Team == Team.White)
        {
            FirstFigure = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub, transform);
            SecondFigure = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub,  transform);
            FirstFigure.Data = _battleInfo.FirstFigure;
            SecondFigure.Data = _battleInfo.SecondFigure;
            FirstFigure.Skill = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub.Skill);
            SecondFigure.Skill = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub.Skill);
            FirstFigure.Talent = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub.Talent);
            SecondFigure.Talent = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub.Talent);
        }
        else
        {
            SecondFigure = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub, transform);
            FirstFigure = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub, transform);
            SecondFigure.Data = _battleInfo.FirstFigure;
            FirstFigure.Data = _battleInfo.SecondFigure;
            FirstFigure.Skill = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub.Skill);
            SecondFigure.Skill = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub.Skill);
            FirstFigure.Talent = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub.Talent);
            SecondFigure.Talent = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub.Talent);
        }

        if (Width % 2 == 0 && Height % 2 == 1)
        {
            StartCoroutine(FirstFigure.MoveToAnotherCellWithAnimation(Cells[0, 0]));
        }
        else if(Width % 2 == 1 && Height % 2 == 0)
        {
            StartCoroutine(FirstFigure.MoveToAnotherCellWithAnimation(Cells[Width - 1, Height - 1]));
        }
        else
        {
            StartCoroutine(FirstFigure.MoveToAnotherCellWithAnimation(Cells[Width - 1, 0]));
        }

        StartCoroutine(SecondFigure.MoveToAnotherCellWithAnimation(Cells[0, Height - 1]));

        DownCastCells();

        GetComponentInParent<Image>().rectTransform.sizeDelta = new Vector2((Width + 2) * _cellWidth, (Height + 2) * _cellHeight);
    }
    
    private void DownCastCells()
    {
        _battleFieldCells = new BattleFieldCell[Cells.GetLength(0), Cells.GetLength(1)];
        for (int x = 0; x < BattleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < BattleFieldCells.GetLength(1); y++)
            {
                _battleFieldCells[x, y] = (BattleFieldCell)Cells[x, y];
            }
        }
    }
}