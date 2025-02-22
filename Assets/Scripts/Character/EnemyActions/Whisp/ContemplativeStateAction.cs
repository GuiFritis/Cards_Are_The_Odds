using System.Collections;
using UnityEngine;

public class ContemplativeStateAction : BaseAction
{   
    [SerializeField] private int _heals;
    private int _healsMade;

    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        _thisCharacter.Health.TakeDamage(-15);
    }

    protected override IEnumerator Success(int result)
    {   
        PlayAudio();
        yield return new WaitForSeconds(_duration);
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

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _thisCharacter.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _thisCharacter.Stun();
    }
}
