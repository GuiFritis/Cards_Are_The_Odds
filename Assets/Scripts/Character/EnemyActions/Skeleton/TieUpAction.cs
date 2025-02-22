using System.Collections;
using UnityEngine;

public class TieUp : BaseAction
{   
    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        PlayAudio();
        _enemyCharacter.Health.TakeDamage(_damage);
    }

    protected override IEnumerator Success(int result)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
        _enemyCharacter.Stun();
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
        _enemyCharacter.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
        _thisCharacter.Stun();
    }
}
