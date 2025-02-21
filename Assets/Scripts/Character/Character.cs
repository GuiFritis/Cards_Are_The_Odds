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
    private bool _onTurn = false;
    public static System.Action<Character> OnTurnStart;
    public static System.Action<Character> OnTurnEnd;
    public System.Action<int> OnStun;
    public System.Action<int> OnAdvantage;

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
        OnTurnStart?.Invoke(this);
        _onTurn = true;
        LoseStun();
        if(_stun > 0)
        {
            EndTurn();
        }
    }

    public void Cleanse()
    {
        _stun = 0;
        OnStun(_stun);
        _advantage = 0;
        OnAdvantage(_advantage);
        _damageOverTime.Clear();
    }

    public void EndTurn()
    {
        _onTurn = false;
        LoseAdvantage();
        OnTurnEnd?.Invoke(this);
    }

    public void Stun()
    {
        if(_stun == 0)
        {
            _stun = 2;
            OnStun?.Invoke(_stun);
        }
    }

    private void LoseStun()
    {
        if(_stun > 0)
        {
            _stun--;
            OnStun?.Invoke(_stun);
        }
    }

    public void GiveAdvantage(int bonus)
    {
        if(_onTurn)
        {
            bonus++;
        }
        _advantage += bonus;
        OnAdvantage?.Invoke(_advantage);
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
        OnAdvantage?.Invoke(_advantage);
    }
}
