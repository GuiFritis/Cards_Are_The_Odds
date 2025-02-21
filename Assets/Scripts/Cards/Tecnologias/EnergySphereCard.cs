public class EnergySphereCard : CardBase
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
                SucessoCritico(result);
                break;
            case var _ when result >= _cardSO.sucesso:
                Sucesso(result);
                break;
            case var _ when result < _cardSO.falha:
                FalhaCritica();
                break;
        }
    }

    private void SucessoCritico(int result)
    {
        _enemy.Health.TakeDamage(_cardSO.dano + result - 10);
        _enemy.Stun();
    }

    private void Sucesso(int result)
    {        
        _enemy.Health.TakeDamage(_cardSO.dano + result - 10);
    }

    private void FalhaCritica()
    {
        _player.Health.TakeDamage(_cardSO.dano/2);
        _player.GiveAdvantage(-2);
    }
}
