using System.Collections;
using UnityEngine;

public class FlamethrowerCard : CardBase
{
    [SerializeField] private int _burningDamage;
    [SerializeField] private int _burningDuration;

    public override bool CanUse()
    {
        return _fuel.Value >= _cardSO.combustivel;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBurnDamage(_burningDuration, _burningDamage + 5);
    }

    protected override IEnumerator Success(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBurnDamage(_burningDuration, _burningDamage);
    }

    protected override IEnumerator Failure(int result = 0)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration/2);
        _enemy.Health.TakeDamage(0);
    }

    protected override IEnumerator CritFailure(int result)
    {
        PlayAudio();
        yield return new WaitForSeconds(_cardSO.duration);
        _player.Health.TakeDamage(_cardSO.dano);
        _player.GiveAdvantage(-1);
    }
}
