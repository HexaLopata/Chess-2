using UnityEngine;

public abstract class TimeLimitedObject : BattleFieldObject
{
    [SerializeField] protected float _turnRemains = 10;
    public override void Execute()
    {
        _turnRemains -= 0.5f;
        if (_turnRemains <= 0)
        {
            DestroyThisBattleFieldObject();
        }
    }
}