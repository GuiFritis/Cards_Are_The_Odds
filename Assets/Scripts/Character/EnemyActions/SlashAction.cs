using UnityEngine;

public class SlashAction : BaseAction
{
    [SerializeField] private int _bleedDamage;
    [SerializeField] private int _bleedDuration;
    

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
        _enemyCharacter.DmgOverTime.AddFireDamage(_bleedDuration, _bleedDamage);
    }

    private void Success()
    {
        _enemyCharacter.Health.TakeDamage(_damage);
    }

    private void CriticalFailure()
    {
        _thisCharacter.Health.TakeDamage(_damage/2);
    }
}
