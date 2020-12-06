using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// Инициализирует набор фигур и скиллов для игроков
/// </summary>
public class DeckInitializer : MonoBehaviour
{
    [SerializeField] private FigureFactory _pawnFactory;
    [SerializeField] private FigureFactory _kingFactory;
    [SerializeField] private FigureFactory _queenFactory;
    [SerializeField] private FigureFactory _bishopFactory;
    [SerializeField] private FigureFactory _rookFactory;
    [SerializeField] private FigureFactory _horseFactory;

    [SerializeField] private List<Skill> _skills = new List<Skill>();

    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        DestroyAllPreviousFigures();
        
        Core.FirstPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(Team.White, GetRandomSkill()),
            King = _kingFactory.GetFigure(Team.White, GetRandomSkill()),
            Queen = _queenFactory.GetFigure(Team.White, GetRandomSkill()),
            Bishop = _bishopFactory.GetFigure(Team.White, GetRandomSkill()),
            Rook = _rookFactory.GetFigure(Team.White, GetRandomSkill()),
            Horse = _horseFactory.GetFigure(Team.White, GetRandomSkill())
        };

        Core.SecondPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(Team.Black, GetRandomSkill()),
            King = _kingFactory.GetFigure(Team.Black, GetRandomSkill()),
            Queen = _queenFactory.GetFigure(Team.Black, GetRandomSkill()),
            Bishop = _bishopFactory.GetFigure(Team.Black, GetRandomSkill()),
            Rook = _rookFactory.GetFigure(Team.Black, GetRandomSkill()),
            Horse = _horseFactory.GetFigure(Team.Black, GetRandomSkill())
        };
        
        if(currentScene.isLoaded)
            SceneManager.SetActiveScene(currentScene);
    }

    private void DestroyAllPreviousFigures()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));

        var currentFigures = FindObjectsOfType<MonoFigure>();
        foreach (var figure in currentFigures)
        {
            Destroy(figure.gameObject);
        }
    }

    private Skill GetRandomSkill()
    {
        return _skills[Random.Range(0, _skills.Count)];
    }
}