using System.Collections;
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
    public static System.Action<CardBase> OnCardActivated;
    public static System.Action<CardBase> OnCardFinished;
    public abstract bool CanUse();
    protected abstract IEnumerator CritSuccess(int result = 0);
    protected abstract IEnumerator Success(int result = 0);
    protected abstract IEnumerator Failure(int result = 0);
    protected abstract IEnumerator CritFailure(int result = 0);

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

    public IEnumerator Activate(int advantage = 0)
    {
        int result = 0;
        OnCardActivated?.Invoke(this);
        if(_cardSO.cardType == CardType.TECNOLOGY)
        {
            _fuel.Value -= _cardSO.combustivel;
        }
        yield return StartCoroutine(Dice.Instance.ThrowDice(i => result = i, advantage, _cardSO.falha, _cardSO.sucesso));
        switch (result)
        {
            case 20:
                yield return StartCoroutine(CritSuccess());
                break;
            case var _ when result >= _cardSO.sucesso:
                yield return StartCoroutine(Success());
                break;
            case var _ when result >= _cardSO.falha:
                yield return StartCoroutine(Failure());
                break;
            case var _ when result < _cardSO.falha:
                yield return StartCoroutine(CritFailure());
                break;
        }
        yield return new WaitForSeconds(.2f);
        OnCardFinished?.Invoke(this);
    }

    protected void PlayAudio()
    {
        SFX_Pool.Instance.Play(_cardSO.soAudio);
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
