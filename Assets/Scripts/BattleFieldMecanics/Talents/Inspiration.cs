using UnityEngine;

public class Inspiration : Talent
{
    [SerializeField] private int _inspirationChance = 10;

    private AudioSource _onActionSound;

    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        if (Random.Range(1, 101) <= _inspirationChance && _owner.Skill.Delay > 0.5f)
        {
            _owner.Skill.Delay = 0;
            _onActionSound.Play();
        }
    }
}
