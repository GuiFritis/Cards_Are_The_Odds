using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BionicKickCard : CardBase
{
    public override bool CanUse()
    {
        return true;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        yield return StartCoroutine(Success(result));
        PlayAudio();
        _enemy.Stun();
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
        _player.Health.TakeDamage(_cardSO.dano/2);
        _player.GiveAdvantage(-1);
    }
}
