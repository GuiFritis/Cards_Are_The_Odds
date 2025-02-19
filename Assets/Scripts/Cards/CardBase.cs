using UnityEngine;

[RequireComponent(typeof(CardUI))]
public abstract class CardBase : MonoBehaviour, ICard
{
    [SerializeField] protected CardSO cardSO;
    public CardSO GetCardSO => cardSO;
    [SerializeField] protected SOInt combustivel;
    protected Character _player;
    protected Character _enemy;
    public static System.Action<CardBase> OnCardUsed;
    public abstract bool CanUse();
    public abstract void Activate(int advantage = 0);

    private void OnEnable()
    {
        if(_player == null)
        {
            _player = GameObject.FindWithTag("Player")?.GetComponent<Character>();
        }
        if(_enemy == null)
        {
            _enemy = GameObject.FindWithTag("Enemy")?.GetComponent<Character>();
        }
    }
}
