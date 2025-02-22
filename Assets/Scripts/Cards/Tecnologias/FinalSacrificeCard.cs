using System.Collections;
using UnityEngine;

public class FinalSacrificeCard : CardBase
{
    public override bool CanUse()
    {
        return _fuel.Value >= _cardSO.combustivel;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration * 1.2f);
        _player.Health.TakeDamage(1);
        _enemy.Health.TakeDamage(Mathf.RoundToInt(_cardSO.dano));
    }

    protected override IEnumerator Success(int result)
    {        
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(10);
        _enemy.Health.TakeDamage(_cardSO.dano);
    }

    protected override IEnumerator Failure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(10);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(18);
    }
}
