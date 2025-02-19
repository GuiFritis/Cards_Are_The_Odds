using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class Character : MonoBehaviour
{
    [SerializeField] private HealthBase _health;
    public HealthBase Health => _health;
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
    }

    public void StartTurn()
    {
        if(_stun > 0)
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        _advantage--;
        OnTurnEnd?.Invoke(this);
    }

    public void Stun()
    {
        if(_stun == 0)
        {
            _stun = 2;
        }
    }

    public void GiveAdvantage(int bonus)
    {
        _advantage += bonus;
    }
}
