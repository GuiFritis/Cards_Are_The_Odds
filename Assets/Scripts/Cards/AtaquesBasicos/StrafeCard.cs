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

    public override void Activate(int advantage = 0)
    {
        int result = Dice.Instance.ThrowDice(advantage);
        OnCardUsed?.Invoke(this);
        switch (result)
        {
            case 20:
                SucessoCritico();
                break;
            case var _ when result >= _cardSO.sucesso:
                Sucesso();
                break;
            case var _ when result < _cardSO.falha:
                FalhaCritica();
                break;
        }
    }

    private void SucessoCritico()
    {
        for (int i = 0; i < _criticalShots; i++)
        {
            _enemy.Health.TakeDamage(_cardSO.dano);
            _fuel.Value += _cardSO.combustivel;
        }
    }

    private void Sucesso()
    {
        for (int i = 0; i < _shots; i++)
        {
            _enemy.Health.TakeDamage(_cardSO.dano);
            _fuel.Value += _cardSO.combustivel;
        }
    }

    private void FalhaCritica()
    {
        for (int i = 0; i < _selfDamageShots; i++)
        {
            _player.Health.TakeDamage(_cardSO.dano);
        }
    }
}
