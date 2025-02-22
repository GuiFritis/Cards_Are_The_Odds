using System.Collections;
using UnityEngine;

public class SlipperyFloorAction : BaseAction
{   
    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        _enemyCharacter.Stun();
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.GiveAdvantage(-2);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_duration);
        _thisCharacter.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _thisCharacter.GiveAdvantage(-1);
    }
}
