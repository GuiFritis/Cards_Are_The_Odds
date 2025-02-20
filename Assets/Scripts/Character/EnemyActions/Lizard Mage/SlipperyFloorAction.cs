public class SlipperyFloorAction : BaseAction
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
        _enemyCharacter.Stun();
        Success();
    }

    private void Success()
    {
        _enemyCharacter.GiveAdvantage(-2);
    }

    private void CriticalFailure()
    {
        _thisCharacter.GiveAdvantage(-1);
    }
}
