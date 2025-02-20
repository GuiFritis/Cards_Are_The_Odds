using UnityEngine;

public class FireBallAction : BaseAction
{   
    [SerializeField] private int _burnDamage = 5;
    [SerializeField] private int _burnDuration = 2;

    public override void Activate(int advantage = 0)
    {
        int result = Dice.Instance.ThrowDice(advantage);
        OnActionUsed?.Invoke(this);
        switch (result)
        {
            case 20:
                CriticalSuccess();
                break;
            case var _ when result >= _success:
                Success();
                break;
            case var _ when result < _failure:
                CriticalFailure();
                break;
        }
    }

    private void CriticalSuccess()
    {
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.DmgOverTime.AddBurnDamage(_burnDuration * 2, _burnDamage);
    }

    private void Success()
    {
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.DmgOverTime.AddBurnDamage(_burnDuration * 2, _burnDamage);
    }

    private void CriticalFailure()
    {
        _thisCharacter.Health.TakeDamage(10);
    }
}
