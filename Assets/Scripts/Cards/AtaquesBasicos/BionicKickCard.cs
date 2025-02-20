public class BionicKickCard : CardBase
{
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
        _enemy.Stun();
        Sucesso();
    }

    private void Sucesso()
    {
        _enemy.Health.TakeDamage(_cardSO.dano);
        _fuel.Value += _cardSO.combustivel;
    }

    private void FalhaCritica()
    {
        _player.Health.TakeDamage(_cardSO.dano/2);
        _player.GiveAdvantage(-1);
    }
}
