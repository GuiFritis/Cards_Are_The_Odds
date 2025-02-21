using System.Collections;
using UnityEngine;

public class StunningDart : CardBase
{
    public override bool CanUse()
    {
        return _fuel.Value >= _cardSO.combustivel;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano * 3);
        _enemy.Stun();
    }

    protected override IEnumerator Success(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.Stun();
    }
    
    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_cardSO.duration);
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration*1.2f);
        _player.Stun();
    }
}
