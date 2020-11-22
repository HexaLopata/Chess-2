using System;
using UnityEngine;

public class MainFieldUIManager : MonoBehaviour, IDisposable
{
    [SerializeField] private Chess2Text _yourTurnText1;
    [SerializeField] private Chess2Text _yourTurnText2;
    [SerializeField] private MainField _mainField;
    private MainFieldTurnManager _turnManager;

    private void Start()
    {
        _turnManager = _mainField.MainFieldTurnManager;
        _turnManager.OnSwitchTurn += ShowCurrentTurn;
    }

    private void ShowCurrentTurn()
    {
        if (_turnManager.CurrentTurn == Team.Black)
        {
            _yourTurnText1.gameObject.SetActive(false);
            _yourTurnText2.gameObject.SetActive(true);
        }
        else
        {
            _yourTurnText1.gameObject.SetActive(true);
            _yourTurnText2.gameObject.SetActive(false);
        }
    }
    
    public void Dispose()
    {
        _turnManager.OnSwitchTurn -= ShowCurrentTurn;
    }
}