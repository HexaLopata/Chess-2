using System;
using UnityEngine;
using UnityEngine.UI;

public class MainFieldUIManager : MonoBehaviour, IDisposable
{
    [SerializeField] private Image _yourTurnImage1;
    [SerializeField] private Image _yourTurnImage2;
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
            _yourTurnImage1.enabled = false;
            _yourTurnImage2.enabled = true;
        }
        else
        {
            _yourTurnImage1.enabled = true;
            _yourTurnImage2.enabled = false;
        }
    }
    
    public void Dispose()
    {
        _turnManager.OnSwitchTurn -= ShowCurrentTurn;
    }
}