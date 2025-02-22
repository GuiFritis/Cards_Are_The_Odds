using System.Collections;
using UnityEngine;

public class MindGaspAction : BaseAction
{   
    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.GiveAdvantage(-3);
    }

    protected override IEnumerator Success(int result)
    {   
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.GiveAdvantage(-2);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _thisCharacter.GiveAdvantage(-2);
    }
}
