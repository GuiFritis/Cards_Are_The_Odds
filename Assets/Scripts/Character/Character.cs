using UnityEngine;

[RequireComponent(typeof(HealthBase), typeof(DamageOverTime))]
public class Character : MonoBehaviour
{
    [SerializeField] private HealthBase _health;
    public HealthBase Health => _health;
    [SerializeField] private DamageOverTime _damageOverTime;
    public DamageOverTime DmgOverTime => _damageOverTime;
    private int _advantage = 0;
    public int Advantage => _advantage;
    private int _stun = 0;
    public bool IsStuned => _stun > 0;
    public static System.Action<Character> OnTurnStart;
    public static System.Action<Character> OnTurnEnd;

    void OnValidate()
    {
        if(_health == null)
        {
            _health = GetComponent<HealthBase>();
        }
        if(_damageOverTime == null)
        {
            _damageOverTime = GetComponent<DamageOverTime>();
        }
    }

    public void StartTurn()
    {
        LoseStun();
        if(_stun > 0)
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        LoseAdvantage();
        OnTurnEnd?.Invoke(this);
    }

    public void Stun()
    {
        if(_stun == 0)
        {
            _stun = 2;
        }
    }

    private void LoseStun()
    {
        if(_stun > 0)
        {
            _stun--;
        }
    }

    public void GiveAdvantage(int bonus)
    {
        _advantage += bonus;
    }

    private void LoseAdvantage()
    {
        if(_advantage > 0)
        {
            _advantage--;
        }
        else if(_advantage < 0)
        {
            _advantage++;
        }
    }
}
