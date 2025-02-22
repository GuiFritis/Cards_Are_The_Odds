using System.Collections;
using UnityEngine;

public class SlashAction : BaseAction
{
    [SerializeField] private int _bleedDamage;
    [SerializeField] private int _bleedDuration;

    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        _enemyCharacter.DmgOverTime.AddBleedDamage(_bleedDuration, _bleedDamage);
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
        _thisCharacter.Health.TakeDamage(_damage/2);
    }
}
