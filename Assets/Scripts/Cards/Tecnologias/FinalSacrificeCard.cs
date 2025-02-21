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
        yield return new WaitForSeconds(_cardSO.duration * 1.2f);
        _player.Health.TakeDamage(5);
        _enemy.Health.TakeDamage(_cardSO.dano);
    }

    protected override IEnumerator Success(int result)
    {        
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(15);
        _enemy.Health.TakeDamage(_cardSO.dano);
    }

    protected override IEnumerator Failure(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(15);
    }

    protected override IEnumerator CritFailure(int result)
    {
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(25);
    }
}
