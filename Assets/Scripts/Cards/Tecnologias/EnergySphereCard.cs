using System.Collections;
using UnityEngine;

public class EnergySphereCard : CardBase
{
    public override bool CanUse()
    {
        return _fuel.Value >= _cardSO.combustivel;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration * 2);
        _enemy.Health.TakeDamage(_cardSO.dano + result - 10);
        _enemy.Stun();
    }

    protected override IEnumerator Success(int result)
    {        
        yield return new WaitForSeconds(_cardSO.duration * result/10);
        _enemy.Health.TakeDamage(_cardSO.dano + result - 10);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_cardSO.duration);
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(_cardSO.dano/2);
        _player.GiveAdvantage(-2);
    }
}
