using System;

/// <summary>
/// Вся информация об игроке
/// </summary>
public class PlayerData
{
    private string _name;
    private Deck _deck;

    public Deck Deck
    {
        get
        {
            return _deck;
        }
        set
        {
            if (value != null)
                _deck = value;
            else
                throw new ArgumentException("Deck can not be null");
        }
    }
}

