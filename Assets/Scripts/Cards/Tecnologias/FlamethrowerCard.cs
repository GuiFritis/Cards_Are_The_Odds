using UnityEngine;

public class FlamethrowerDart : CardBase
{
    [SerializeField] private int _burningDamage;
    [SerializeField] private int _burningDuration;

    public override bool CanUse()
    {
        return _fuel.Value >= _cardSO.combustivel;
    }

    public override void Activate(int advantage = 0)
    {
        _fuel.Value -= _cardSO.combustivel;
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
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBleedDamage(_burningDuration, _burningDamage + 5);
    }

    private void Sucesso()
    {
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddBleedDamage(_burningDuration, _burningDamage);
    }

    private void FalhaCritica()
    {
        _player.Health.TakeDamage(_cardSO.dano);
        _player.GiveAdvantage(-1);
    }
}
