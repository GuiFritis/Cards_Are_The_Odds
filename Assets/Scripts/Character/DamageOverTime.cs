using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private List<DamageTurns> _damageOnTurnStart = new();
    private List<DamageTurns> _damageOnTurnEnd = new();
    private int _currentTurn = 0;

    void Awake()
    {
        Character.OnTurnStart += TurnStart;
        Character.OnTurnEnd += TurnEnd;
    }

    public void Clear()
    {
        _damageOnTurnStart.Clear();
        _damageOnTurnEnd.Clear();
    }

    private void TurnStart(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            _currentTurn++;
            foreach (DamageTurns damageTurn in _damageOnTurnStart)
            {
                character.Health.TakeDamage(damageTurn.damage);
            }
            _damageOnTurnStart.RemoveAll(i => i.duration >= _currentTurn - i.startTurn);
        }
    }

    private void TurnEnd(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            _currentTurn++;
            foreach (DamageTurns damageTurn in _damageOnTurnStart)
            {
                character.Health.TakeDamage(damageTurn.damage);
            }
            _damageOnTurnStart.RemoveAll(i => i.duration >= _currentTurn - i.startTurn);
        }
    }

    public void AddBurnDamage(int duration, int damage)
    {
        _damageOnTurnStart.Add(new DamageTurns(_currentTurn, duration, damage));
    }

    public void AddBleedDamage(int duration, int damage)
    {
        _damageOnTurnEnd.Add(new DamageTurns(_currentTurn, duration, damage));
    }
}

public struct DamageTurns
{
    public int startTurn;
    public int duration;
    public int damage;

    public DamageTurns(int startTurn, int duration, int damage)
    {
        this.startTurn = startTurn;
        this.duration = duration;
        this.damage = damage;
    }
}
