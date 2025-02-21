public class FinalSacrificeCard : CardBase
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
            case var _ when result >= _cardSO.falha:
                Falha();
                break;
            case var _ when result < _cardSO.falha:
                FalhaCritica();
                break;
        }
    }

    private void SucessoCritico()
    {
        _player.Health.TakeDamage(5);
        _enemy.Health.TakeDamage(_cardSO.dano);
    }

    private void Sucesso()
    {        
        _player.Health.TakeDamage(15);
        _enemy.Health.TakeDamage(_cardSO.dano);
    }

    private void Falha()
    {
        _player.Health.TakeDamage(15);
    }

    private void FalhaCritica()
    {
        _player.Health.TakeDamage(25);
    }
}
