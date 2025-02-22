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
            PlayAudio();
            yield return new WaitForSeconds(_duration);
            _enemyCharacter.Health.TakeDamage(_damage);
        }
    }

    protected override IEnumerator Success(int result)
    {   
        for (int i = 0; i < _missilesOnSuccess; i++)
        {
            PlayAudio();
            yield return new WaitForSeconds(_duration);
            _enemyCharacter.Health.TakeDamage(_damage);
        }
    }

    protected override IEnumerator Failure(int result = 0)
    {
        for (int i = 0; i < _missilesOnSuccess-1; i++)
        {
            PlayAudio();
            yield return new WaitForSeconds(_duration);
        }
    }

    protected override IEnumerator CritFailure(int result)
    {
        for (int i = 0; i < _missilesOnSuccess; i++)
        {
            PlayAudio();
            yield return new WaitForSeconds(_duration/2);
            _thisCharacter.Health.TakeDamage(_damage);
        }
    }
}
