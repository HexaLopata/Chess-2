using UnityEngine;
using UnityEngine.EventSystems;

public class FigureSelector : MonoBehaviour, IPointerClickHandler
{
    public FigureData Figure { get => _figure; set => _figure = value; }

    [SerializeField] private DeckSelectUIManager _uIManager;
    [SerializeField] private Team _team;
    [SerializeField] private FigureType _figureType;

    private FigureData _figure;

    private void Awake()
    {
        PlayerData player = _team == Team.White ? Core.FirstPlayerData : Core.SecondPlayerData;

        switch (_figureType)
        {
            case FigureType.Pawn:
                _figure = player.Deck.Pawn;
                break;
            case FigureType.Horse:
                _figure = player.Deck.Horse;
                break;
            case FigureType.Bishop:
                _figure = player.Deck.Bishop;
                break;
            case FigureType.Rook:
                _figure = player.Deck.Rook;
                break;
            case FigureType.Queen:
                _figure = player.Deck.Queen;
                break;
            case FigureType.King:
                _figure = player.Deck.King;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uIManager != null)
        {
            _uIManager.UpdateInfo(_figure);
        }
    }
}