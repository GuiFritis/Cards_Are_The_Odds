using System.Collections;
using UnityEngine;

public class FireBallAction : BaseAction
{   
    [SerializeField] private int _burnDamage = 5;
    [SerializeField] private int _burnDuration = 2;

    protected override IEnumerator CritSuccess(int result)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.DmgOverTime.AddBurnDamage(_burnDuration * 2, _burnDamage);
    }

    protected override IEnumerator Success(int result)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
        _enemyCharacter.Health.TakeDamage(_damage);
        _enemyCharacter.DmgOverTime.AddBurnDamage(_burnDuration * 2, _burnDamage);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_duration);
        PlayAudio();
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_duration*0.8f);
        PlayAudio();
        _thisCharacter.Health.TakeDamage(10);
    }
}
