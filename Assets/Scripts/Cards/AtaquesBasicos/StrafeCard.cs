using System.Collections;
using UnityEngine;

public class StrafeCard : CardBase
{
    [SerializeField] private int _shots = 3;
    [SerializeField] private int _criticalShots = 4;
    [SerializeField] private int _selfDamageShots = 2;

    public override bool CanUse()
    {
        return true;
    }

    protected override IEnumerator CritSuccess(int result)
    {
        for (int i = 0; i < _criticalShots; i++)
        {
            yield return new WaitForSeconds(_cardSO.duration);
            PlayAudio();
            _enemy.Health.TakeDamage(_cardSO.dano + 1);
            _fuel.Value += _cardSO.combustivel;
        }
    }

    protected override IEnumerator Success(int result)
    {
        for (int i = 0; i < _shots; i++)
        {
            yield return new WaitForSeconds(_cardSO.duration);
            PlayAudio();
            _enemy.Health.TakeDamage(_cardSO.dano);
            _fuel.Value += _cardSO.combustivel;
        }
    }

    protected override IEnumerator Failure(int result = 0)
    {
        for (int i = 0; i < _shots - 1; i++)
        {
            yield return new WaitForSeconds(_cardSO.duration);
            PlayAudio();
            _enemy.Health.TakeDamage(0);
        }
    }

    protected override IEnumerator CritFailure(int result)
    {
        for (int i = 0; i < _selfDamageShots; i++)
        {
            PlayAudio();
            _player.Health.TakeDamage(_cardSO.dano);
            yield return new WaitForSeconds(_cardSO.duration);
        }
    }
}
