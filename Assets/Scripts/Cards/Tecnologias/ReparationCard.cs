using System;
using System.Collections;
using UnityEngine;

public class ReparationCard : CardBase
{
    public override bool CanUse()
    {
        return _fuel.Value >= _cardSO.combustivel;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        _player.Cleanse();
        _player.GiveAdvantage(1);
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(_cardSO.dano);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration/2);
        _player.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration*1.2f);
        _player.Health.TakeDamage(5);
        _player.GiveAdvantage(-1);
    }
}
