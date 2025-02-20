using UnityEngine;

public class ContemplativeStateAction : BaseAction
{   
    [SerializeField] private int _heals;
    private int _healsMade;
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
        _thisCharacter.Health.TakeDamage(-15);
        Success();
    }

    private void Success()
    {   
        _thisCharacter.Stun();
        Character.OnTurnEnd += Heal;
    }

    private void Heal(Character character)
    {
        if(character.Equals(_thisCharacter))
        {
            _thisCharacter.Health.TakeDamage(_damage);
            _healsMade++;
            if(_heals >= _healsMade)
            {
                Character.OnTurnEnd -= Heal;
            }
        }
    }

    private void CriticalFailure()
    {
        _thisCharacter.Stun();
    }
}
