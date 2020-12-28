using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private List<Talent> _talents = new List<Talent>();

    private void Start()
    {       
        Core.FirstPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(Team.White, GetRandomSkill(), GetRandomTalent()),
            King = _kingFactory.GetFigure(Team.White, GetRandomSkill(), GetRandomTalent()),
            Queen = _queenFactory.GetFigure(Team.White, GetRandomSkill(), GetRandomTalent()),
            Bishop = _bishopFactory.GetFigure(Team.White, GetRandomSkill(), GetRandomTalent()),
            Rook = _rookFactory.GetFigure(Team.White, GetRandomSkill(), GetRandomTalent()),
            Horse = _horseFactory.GetFigure(Team.White, GetRandomSkill(), GetRandomTalent())
        };

        Core.SecondPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(Team.Black, GetRandomSkill(), GetRandomTalent()),
            King = _kingFactory.GetFigure(Team.Black, GetRandomSkill(), GetRandomTalent()),
            Queen = _queenFactory.GetFigure(Team.Black, GetRandomSkill(), GetRandomTalent()),
            Bishop = _bishopFactory.GetFigure(Team.Black, GetRandomSkill(), GetRandomTalent()),
            Rook = _rookFactory.GetFigure(Team.Black, GetRandomSkill(), GetRandomTalent()),
            Horse = _horseFactory.GetFigure(Team.Black, GetRandomSkill(), GetRandomTalent())
        };
    }

    private Skill GetRandomSkill()
    {
        return _skills[Random.Range(0, _skills.Count)];
    }

    private Talent GetRandomTalent()
    {
        return _talents[Random.Range(0, _talents.Count)];
    }
}