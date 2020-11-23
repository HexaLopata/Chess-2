﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleField : FieldBase
{
    #region public Properties

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
            }
        }
    }

    #endregion

    #region private Fields

    private BattleInfo _battleInfo;
    private BattleFieldCell[,] _battleFieldCells;

    [SerializeField] private BattleController _battleController;

    #endregion

    #region public Methods

    public void ActivateAllCells(BattleFieldCell[] battleFieldCells)
    {
        DeactivateAllCells();
        foreach (var cell in battleFieldCells)
        {
            if(cell != null)
                cell.Activate();
        }
    }
    public void DeactivateAllCells()
    {
        if (BattleFieldCells != null)
        {
            foreach (var cell in BattleFieldCells)
            {
                if(cell != null)
                    cell.Deactivate();
            }
        }
    }

    #endregion

    #region protected Methods

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
        Team firstTurn;
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
            firstTurn = Team.White;
        }
        else
        {
            SecondFigure = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub, transform);
            FirstFigure = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub, transform);
            SecondFigure.Data = _battleInfo.FirstFigure;
            FirstFigure.Data = _battleInfo.SecondFigure;
            FirstFigure.Skill = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub.Skill);
            SecondFigure.Skill = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub.Skill);
            firstTurn = Team.Black;
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

        BattleFieldCells = new BattleFieldCell[Cells.GetLength(0), Cells.GetLength(1)];
        for (int x = 0; x < BattleFieldCells.GetLength(0); x++)
        {
            for (int y = 0; y < BattleFieldCells.GetLength(1); y++)
            {
                BattleFieldCells[x, y] = (BattleFieldCell)Cells[x, y];
            }
        }
        _battleController.SwitchTurn(firstTurn);
        
        GetComponentInParent<Image>().rectTransform.sizeDelta = new Vector2((Width + 2) * _cellWidth, (Height + 2) * _cellHeight);
    }

    #endregion
}
