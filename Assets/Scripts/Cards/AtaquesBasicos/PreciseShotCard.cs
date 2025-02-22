using System.Collections;
using UnityEngine;

public class PreciseShotCard : CardBase
{
    public override bool CanUse()
    {
        return true;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration * 1.2f);
        _enemy.Health.TakeDamage(Mathf.RoundToInt(_cardSO.dano * 1.5f));
        _fuel.Value += _cardSO.combustivel;
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _fuel.Value += _cardSO.combustivel;
    }

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _fuel.Value--;
        _enemy.GiveAdvantage(1);
    }
}
