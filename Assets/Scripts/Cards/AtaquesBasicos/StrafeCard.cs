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
            _enemy.Health.TakeDamage(_cardSO.dano);
            _fuel.Value += _cardSO.combustivel;
            yield return new WaitForSeconds(_cardSO.duration);
        }
    }

    protected override IEnumerator Success(int result)
    {
        for (int i = 0; i < _shots; i++)
        {
            _enemy.Health.TakeDamage(_cardSO.dano);
            _fuel.Value += _cardSO.combustivel;
            yield return new WaitForSeconds(_cardSO.duration);
        }
    }

    protected override IEnumerator Failure(int result = 0)
    {
        yield return new WaitForSeconds(_cardSO.duration * 3);
    }

    protected override IEnumerator CritFailure(int result)
    {
        for (int i = 0; i < _selfDamageShots; i++)
        {
            yield return new WaitForSeconds(_cardSO.duration);
            _player.Health.TakeDamage(_cardSO.dano);
        }
    }
}
