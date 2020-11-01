using UnityEngine;

public class DeckInitializer : MonoBehaviour
{
    [SerializeField] private FigureFactory _pawnFactory;
    [SerializeField] private FigureFactory _kingFactory;
    [SerializeField] private FigureFactory _queenFactory;
    [SerializeField] private FigureFactory _bishopFactory;
    [SerializeField] private FigureFactory _rookFactory;
    [SerializeField] private FigureFactory _horseFactory;

    public void Start()
    {
        Core.firstPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(Team.White),
            King = _kingFactory.GetFigure(Team.White),
            Queen = _queenFactory.GetFigure(Team.White),
            Bishop = _bishopFactory.GetFigure(Team.White),
            Rook = _rookFactory.GetFigure(Team.White),
            Horse = _horseFactory.GetFigure(Team.White)
        };

        Core.secondPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(Team.Black),
            King = _kingFactory.GetFigure(Team.Black),
            Queen = _queenFactory.GetFigure(Team.Black),
            Bishop = _bishopFactory.GetFigure(Team.Black),
            Rook = _rookFactory.GetFigure(Team.Black),
            Horse = _horseFactory.GetFigure(Team.Black)
        };
    }
}