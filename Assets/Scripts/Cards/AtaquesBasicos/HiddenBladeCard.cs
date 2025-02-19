using UnityEngine;

public class HiddenBladeCard : CardBase
{
    [SerializeField] private int _bleedDamage;
    [SerializeField] private int _bleedDuration;

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
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.DmgOverTime.AddDamageOnTurnStart(_bleedDuration, _bleedDamage);
        _fuel.Value += _cardSO.combustivel;
    }

    private void Sucesso()
    {
        _enemy.Health.TakeDamage(_cardSO.dano);
        _fuel.Value += _cardSO.combustivel;
    }

    private void FalhaCritica()
    {
        _player.Health.TakeDamage(_cardSO.dano/2);
    }
}
