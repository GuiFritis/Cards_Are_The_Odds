using System.Collections;
using UnityEngine;

public class PistolAction : BaseAction
{   
    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage * 2);
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
        _enemyCharacter.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_duration * 1.2f);
        PlayAudio();
        _thisCharacter.GiveAdvantage(-1);
    }
}
