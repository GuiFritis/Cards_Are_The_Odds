using UnityEngine;

[RequireComponent(typeof(CardUI))]
public abstract class CardBase : MonoBehaviour, ICard
{
    [SerializeField] protected CardSO _cardSO;
    public CardSO GetCardSO => _cardSO;
    [SerializeField] protected SOInt _fuel;
    protected Character _player;
    protected Character _enemy;
    private CardUI _cardUI;
    public CardUI GetCardUI => _cardUI;
    public static System.Action<CardBase> OnCardUsed;
    public abstract bool CanUse();
    public abstract void Activate(int advantage = 0);

    void Awake()
    {
        if(_cardUI == null)
        {
            _cardUI = GetComponent<CardUI>();
        }
        GameManager.OnEnemySpawned += SetEnemy;
    }

    void Start()
    {
        if(_cardUI == null)
        {
            _cardUI = GetComponent<CardUI>();
        }
    }

    private void OnEnable()
    {
        _player = GameManager.Instance.GetPlayer;
        _enemy = GameManager.Instance.GetEnemy;
    }

    private void SetEnemy(Character character)
    {
        _enemy = character;
    }
}
