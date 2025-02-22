using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] private GameObject _burningIcon;
    [SerializeField] private GameObject _bleedingIcon;
    private List<DamageTurns> _damageOnTurnStart = new();
    private List<DamageTurns> _damageOnTurnEnd = new();
    private int _currentTurn = 0;

    void OnEnable()
    {
        Character.OnTurnStart += TurnStart;
        Character.OnTurnEnd += TurnEnd;
    }

    void OnDisable()
    {
        Character.OnTurnStart -= TurnStart;
        Character.OnTurnEnd -= TurnEnd;        
    }

    public void Clear()
    {
        _damageOnTurnStart.Clear();
        _damageOnTurnEnd.Clear();
    }

    private void TurnStart(Character character)
    {
        if(character.gameObject.Equals(gameObject) && _damageOnTurnStart.Count > 0)
        {
            _currentTurn++;
            foreach (DamageTurns damageTurn in _damageOnTurnStart)
            {
                character.Health.TakeDamage(damageTurn.damage);
            }
            _damageOnTurnStart.RemoveAll(i => i.duration < _currentTurn - i.startTurn);
            if(_damageOnTurnStart.Count == 0)
            {
                _burningIcon.SetActive(false);
            }
        }
    }

    private void TurnEnd(Character character)
    {
        if(character.gameObject.Equals(gameObject) && _damageOnTurnEnd.Count > 0)
        {
            _currentTurn++;
            foreach (DamageTurns damageTurn in _damageOnTurnEnd)
            {
                character.Health.TakeDamage(damageTurn.damage);
            }
            _damageOnTurnEnd.RemoveAll(i => i.duration < _currentTurn - i.startTurn);
            if(_damageOnTurnEnd.Count == 0)
            {
                _bleedingIcon.SetActive(false);
            }
        }
    }

    public void AddBurnDamage(int duration, int damage)
    {
        _damageOnTurnStart.Add(new DamageTurns(_currentTurn, duration, damage));
        _burningIcon.SetActive(true);
    }

    public void AddBleedDamage(int duration, int damage)
    {
        _damageOnTurnEnd.Add(new DamageTurns(_currentTurn, duration, damage));
        _bleedingIcon.SetActive(true);
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
