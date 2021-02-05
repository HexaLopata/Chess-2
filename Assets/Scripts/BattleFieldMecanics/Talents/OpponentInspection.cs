using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInspection : Talent
{
    private int _baseOwnerDefence = -1;
    [SerializeField] private float _defencePerTurn = 0.5f;
    protected override void TalentAction()
    {
        if(_baseOwnerDefence == -1)
           _baseOwnerDefence = _owner.Defence;
        _owner.Defence = (int)(_baseOwnerDefence + _controller.TurnCount * _defencePerTurn);
    }
}
