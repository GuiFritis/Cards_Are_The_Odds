using System.Collections;
using UnityEngine;

public class HiddenBladeCard : CardBase
{
    public override bool CanUse()
    {
        return true;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBleedDamage(3, 6);
        _fuel.Value += _cardSO.combustivel;
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBleedDamage(2, 5);
        _fuel.Value += _cardSO.combustivel;
    }

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(4);
        _player.DmgOverTime.AddBleedDamage(2, 4);
    }
}
