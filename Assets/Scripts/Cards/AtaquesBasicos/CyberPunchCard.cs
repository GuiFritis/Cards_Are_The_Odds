public class CyberPunchCard : CardBase
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
            case var _ when result >= cardSO.sucesso:
                Sucesso();
                break;
            case var _ when result < cardSO.falha:
                FalhaCritica();
                break;
        }
    }

    private void SucessoCritico()
    {
        _enemy.Health.TakeDamage(cardSO.dano * 2);
        combustivel.Value += ((CardAtaqueBasicoSO)cardSO).combustivelGerado;
    }

    private void Sucesso()
    {
        _enemy.Health.TakeDamage(cardSO.dano);
        combustivel.Value += ((CardAtaqueBasicoSO)cardSO).combustivelGerado;
    }

    private void FalhaCritica()
    {
        if(combustivel.Value > 0)
        {
            combustivel.Value--;
        }
    }
}
