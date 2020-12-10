using UnityEngine;

public class Vampire : Talent
{
    [SerializeField] private int _heal = 20;
    private AudioSource _onActionSound;

    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        if (_controller.CurrentFigure.Health <= 0 || _controller.CurrentFigure == null)
        {
            _owner.Health += _heal;
            _onActionSound.Play();
        }
    }
}

