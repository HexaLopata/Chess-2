using UnityEngine;

public class Ankh : Talent
{
    private bool _hasUsed = false;
    private AudioSource _onActionSound;

    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        if(!_hasUsed)
            _owner.IsInvulnerable = true;
        if (_owner.Health <= 0 && !_hasUsed)
        {
            _onActionSound.Play();
            _owner.IsInvulnerable = false;
            _hasUsed = true;
        }
    }
}
