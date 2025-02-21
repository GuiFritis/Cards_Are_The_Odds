using System.Collections;
using UnityEngine;

public class SlashAction : BaseAction
{
    [SerializeField] private int _bleedDamage;
    [SerializeField] private int _bleedDuration;

    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        _enemyCharacter.DmgOverTime.AddBurnDamage(_bleedDuration, _bleedDamage);
    }

    protected override IEnumerator Success(int result)
    {
        yield return new WaitForSeconds(_duration);
        _enemyCharacter.Health.TakeDamage(_damage);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_duration);        
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_duration);
        _thisCharacter.Health.TakeDamage(_damage/2);
    }
}
