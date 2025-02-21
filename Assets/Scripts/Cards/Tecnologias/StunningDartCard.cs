public class StunningDart : CardBase
{
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
        _enemy.Health.TakeDamage(_cardSO.dano * 3);
        _enemy.Stun();
    }

    private void Sucesso()
    {
        _enemy.Health.TakeDamage(_cardSO.dano);
        _enemy.Stun();
    }

    private void FalhaCritica()
    {
        _player.Stun();
    }
}
