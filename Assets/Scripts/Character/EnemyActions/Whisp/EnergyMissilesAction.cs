using System.Collections;
using UnityEngine;

public class EnergyMissilesAction : BaseAction
{   
    [SerializeField] private int _missilesOnSuccess;
    [SerializeField] private int _missilesOnCritSuccess;
    [SerializeField] private int _missilesOnCritFailure;

    protected override IEnumerator CritSuccess(int result)
    {
        for (int i = 0; i < _missilesOnCritSuccess; i++)
        {
            yield return new WaitForSeconds(_duration);
            _enemyCharacter.Health.TakeDamage(_damage);
        }
    }

    protected override IEnumerator Success(int result)
    {   
        for (int i = 0; i < _missilesOnSuccess; i++)
        {
            yield return new WaitForSeconds(_duration);
            _enemyCharacter.Health.TakeDamage(_damage);
        }
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_duration * 3);
    }

    protected override IEnumerator CritFailure(int result)
    {
        for (int i = 0; i < _missilesOnSuccess; i++)
        {
            yield return new WaitForSeconds(_duration/2);
            _thisCharacter.Health.TakeDamage(_damage);
        }
    }
}
