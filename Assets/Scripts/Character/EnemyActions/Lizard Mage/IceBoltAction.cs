using System.Collections;
using UnityEngine;

public class IceBoltAction : BaseAction
{   
    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(Mathf.RoundToInt(_damage * 1.5f));
        _enemyCharacter.GiveAdvantage(-1);

    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _thisCharacter.GiveAdvantage(-1);
    }
}
