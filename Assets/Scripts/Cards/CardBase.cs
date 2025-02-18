using UnityEngine;

[RequireComponent(typeof(CardUI))]
public abstract class CardBase : MonoBehaviour, ICard
{
    [SerializeField] protected CardSO cardSO;
    public CardSO GetCardSO => cardSO;
    [SerializeField] protected SOInt combustivel;
    protected HealthBase _playerHealth;
    protected HealthBase _enemyHealth;
    public System.Action OnCardUsed;

    public abstract bool CanUse();
    public abstract void Use();

    private void OnEnable()
    {
        if(_playerHealth == null)
        {
            _playerHealth = GameObject.FindWithTag("Player")?.GetComponent<HealthBase>();
        }
        if(_enemyHealth == null)
        {
            _enemyHealth = GameObject.FindWithTag("Enemy")?.GetComponent<HealthBase>();
        }
    }
}
