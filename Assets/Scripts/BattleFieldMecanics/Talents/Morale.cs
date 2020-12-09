using UnityEngine;

public class Morale : Talent
{
    [SerializeField] private int _moraleChance = 15;

    private AudioSource _onActionSound;

    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        if (Random.Range(1, 101) <= _moraleChance)
        {
            _controller.SwitchTurn(_team);
            _onActionSound.Play();
        }
    }
}