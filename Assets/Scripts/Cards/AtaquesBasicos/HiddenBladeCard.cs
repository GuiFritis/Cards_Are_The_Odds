using System.Collections;
using UnityEngine;

public class HiddenBladeCard : CardBase
{
    [SerializeField] private int _bleedDamage;
    [SerializeField] private int _bleedDuration;

    public override bool CanUse()
    {
        return true;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBurnDamage(_bleedDuration, _bleedDamage);
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
        _player.Health.TakeDamage(_cardSO.dano/2);
    }
}
