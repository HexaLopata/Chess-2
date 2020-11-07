using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    private BattleInfo _battleInfo;
    private BattleFieldFigure _firstFigure;
    private BattleFieldFigure _secondFigure;

    public event Action BattleEnd;

    private void Start()
    {
        // Сделать сцену активной пришлось именно здесь, иначе возникал ArgumentException - invalid scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
        _battleInfo = Core.BattleInfo;
        
        _firstFigure = Instantiate(_battleInfo.FirstFigure.BattleFieldFigurePrefub);
        _secondFigure = Instantiate(_battleInfo.SecondFigure.BattleFieldFigurePrefub);

        _firstFigure.Data = _battleInfo.FirstFigure;
        _secondFigure.Data = _battleInfo.SecondFigure;
    }

    public void SetBattleResult(Team team)
    {
        if (_firstFigure.Data.Team == team)
        {
            _battleInfo.Loser = _secondFigure.Data;
            Destroy(_secondFigure);
            _battleInfo.Winner = _firstFigure.Data;
        }
        else
        {
            _battleInfo.Loser = _firstFigure.Data;
            Destroy(_firstFigure);
            _battleInfo.Winner = _secondFigure.Data;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(3));
    }
}