using UnityEngine;

public class Manipulator : Talent
{
    [SerializeField] private int _manipulationChance = 7;

    private AudioSource _onActionSound;

    private void Start()
    {
        _onActionSound = GetComponent<AudioSource>();
    }

    protected override void TalentAction()
    {
        if(Random.Range(1, 101) <= _manipulationChance)
        {
            BattleFieldCell[] _enemyMoves = _controller.CurrentFigure.GetRelevantMoves();
            var randomCell = _enemyMoves[Random.Range(0, _enemyMoves.Length)];
            _controller.CurrentFigure.Turn(randomCell);
            _onActionSound.Play();
        }
    }
}
