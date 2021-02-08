using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchillesHeel : Talent
{
    private int _ownerBaseDamage = -1;
    private AudioSource _onActionSound;
    [SerializeField] private int _doubleDamageChance = 10;
    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        if(_ownerBaseDamage == -1)
            _ownerBaseDamage = _owner.Damage;

        if(Random.Range(0, 101) < _doubleDamageChance)
        {
            _onActionSound.Play();
            _owner.Damage = _ownerBaseDamage * 2; 
        }
        else
            _owner.Damage = _ownerBaseDamage;
    }
}
