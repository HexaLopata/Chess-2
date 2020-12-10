using UnityEngine;

public class Morale : Talent
{
    [SerializeField] private int _moraleChance = 15;

    private AudioSource _onActionSound;
    private static bool _alreadyUsed; // Если на предыдущий ход эта способность уже работала, то эта переменная будет true
    private static Morale _alreadyUsedBy;

    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        // Если этот талант уже был использован на прошлый ход, то выполнить его еще раз нельзя
        if (!_alreadyUsed)
        {
            if (Random.Range(1, 101) <= _moraleChance)
            {
                _alreadyUsedBy = this;
                _alreadyUsed = true;
                _controller.SwitchTurn(_team);
                _onActionSound.Play();
            }
        }
        else
        {
            // Эта проверка нужна, чтобы изменить переменную _alreadyUsed на false мог только тот талант, который назначил ей true
            if (ReferenceEquals(_alreadyUsedBy, this))
                _alreadyUsed = false;
        }
    }
}