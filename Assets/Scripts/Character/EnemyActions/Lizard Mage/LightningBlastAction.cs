using System.Collections;
using UnityEngine;

public class LightningBlastAction : BaseAction
{   
    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration*1.2f);
        _enemyCharacter.Health.TakeDamage(_damage * 2);
        _enemyCharacter.Stun();
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.Stun();
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
        _thisCharacter.Health.TakeDamage(_damage / 2);
        _thisCharacter.Stun();
    }
}
