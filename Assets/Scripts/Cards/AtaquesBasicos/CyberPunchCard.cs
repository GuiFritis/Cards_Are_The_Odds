public class CyberPunchCard : CardBase
{

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        int result = Dice.Instance.ThrowDice();
        OnCardUsed?.Invoke();
        if(result == 20)
        {
            SucessoCritico();
        }
        else if(result >= cardSO.sucesso)
        {
            Sucesso();
        }
        else if(result < cardSO.falha)
        {
            FalhaCritica();
        }
    }

    private void SucessoCritico()
    {
        _enemyHealth.TakeDamage(cardSO.dano * 2);
        combustivel.Value += ((CardAtaqueBasicoSO)cardSO).combustivelGerado;
    }

    private void Sucesso()
    {
        _enemyHealth.TakeDamage(cardSO.dano);
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
