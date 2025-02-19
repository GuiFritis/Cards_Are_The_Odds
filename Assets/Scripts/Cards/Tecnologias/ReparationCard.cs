using UnityEngine;

public class ReparationCard : CardBase
{
    public override bool CanUse()
    {
        return _fuel.Value > _cardSO.combustivel;
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
        _player.Health.TakeDamage(_cardSO.dano - 10);
        _player.GiveAdvantage(1);
    }

    private void Sucesso()
    {
        _player.Health.TakeDamage(_cardSO.dano);
    }

    private void FalhaCritica()
    {
        _player.Health.TakeDamage(5);
        _player.GiveAdvantage(-1);
    }
}
