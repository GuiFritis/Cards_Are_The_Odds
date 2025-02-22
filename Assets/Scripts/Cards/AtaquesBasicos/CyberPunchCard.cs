using System.Collections;
using UnityEngine;

public class CyberPunchCard : CardBase
{
    public override bool CanUse()
    {
        return true;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        PlayAudio();
        _enemy.Health.TakeDamage(_cardSO.dano * 2);
        _fuel.Value += _cardSO.combustivel;
    }

    protected override IEnumerator Success(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        PlayAudio();
        _enemy.Health.TakeDamage(_cardSO.dano);
        _fuel.Value += _cardSO.combustivel;
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        PlayAudio();
        _enemy.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        PlayAudio();
        if(_fuel.Value > 0)
        {
            _fuel.Value--;
        }
    }
}
