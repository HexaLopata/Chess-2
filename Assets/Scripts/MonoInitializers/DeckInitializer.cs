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
            Pawn = _pawnFactory.GetFigure(),
            King = _kingFactory.GetFigure(),
            Queen = _queenFactory.GetFigure(),
            Bishop = _bishopFactory.GetFigure(),
            Rook = _rookFactory.GetFigure(),
            Horse = _horseFactory.GetFigure()
        };

        Core.secondPlayerData.Deck = new Deck()
        {
            Pawn = _pawnFactory.GetFigure(),
            King = _kingFactory.GetFigure(),
            Queen = _queenFactory.GetFigure(),
            Bishop = _bishopFactory.GetFigure(),
            Rook = _rookFactory.GetFigure(),
            Horse = _horseFactory.GetFigure()
        };
    }
}