public class PistolAction : BaseAction
{   
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
        _enemyCharacter.Health.TakeDamage(_damage * 2);
    }

    private void Success()
    {
        _enemyCharacter.Health.TakeDamage(_damage);
    }

    private void CriticalFailure()
    {
        _thisCharacter.GiveAdvantage(-1);
    }
}
