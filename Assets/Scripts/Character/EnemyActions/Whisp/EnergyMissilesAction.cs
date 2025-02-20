using UnityEngine;

public class EnergyMissilesAction : BaseAction
{   
    [SerializeField] private int _missilesOnSuccess;
    [SerializeField] private int _missilesOnCritSuccess;
    [SerializeField] private int _missilesOnCritFailure;
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
        for (int i = 0; i < _missilesOnCritSuccess; i++)
        {
            _enemyCharacter.Health.TakeDamage(_damage);
        }
    }

    private void Success()
    {   
        for (int i = 0; i < _missilesOnSuccess; i++)
        {
            _enemyCharacter.Health.TakeDamage(_damage);
        }
    }

    private void CriticalFailure()
    {
        for (int i = 0; i < _missilesOnSuccess; i++)
        {
            _thisCharacter.Health.TakeDamage(_damage);
        }
    }
}
