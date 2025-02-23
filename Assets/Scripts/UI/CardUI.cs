using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float _transitionDuration = .4f;
    [SerializeField] private float _targetPositionY = 80f;
    [SerializeField] private float _scaleChange = 1.3f; 
    [SerializeField] private CardBase _cardBase;
    private InitialPosition _initialPosition;
    private bool _enabled = false;
    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardDescription;
    [SerializeField] private TextMeshProUGUI _successValue;
    [SerializeField] private TextMeshProUGUI _failureValue;
    void OnValidate()
    {
        if(_cardBase == null)
        {
            _cardBase = GetComponent<CardBase>();
        }
    }

    void Awake()
    {
        if(_cardBase == null)
        {
            _cardBase = GetComponent<CardBase>();
        }
        CardBase.OnCardFinished += HideCard;
    }

    public void Disable()
    {
        _enabled = false;
        transform.DOMoveY(-30f, _transitionDuration).SetRelative(true).SetEase(Ease.OutQuad);
    }

    public void Enable()
    {
        _enabled = true;
    }

    private void HideCard(CardBase card)
    {
        if(card.Equals(_cardBase))
        {
            transform.DOKill();
            transform.DOScale(0, _transitionDuration);
        }
    }

    public void SetPosition(Vector3 position, Quaternion rotation, int index)
    {
        transform.DOLocalMove(position, _transitionDuration).SetEase(Ease.OutSine);
        transform.DORotate(rotation.eulerAngles, _transitionDuration).SetEase(Ease.OutSine);
        transform.DOScale(1, _transitionDuration).SetEase(Ease.OutSine);
        _initialPosition = new InitialPosition{
            position = position,
            rotation = rotation,
            index = index
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_enabled && _cardBase.CanUse())
        {
            transform.DOScale(1.32f, _transitionDuration);
            StartCoroutine(_cardBase.Activate(GameManager.Instance.GetPlayer.Advantage));
            enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DORotate(Vector3.zero, _transitionDuration).SetEase(Ease.OutSine);
        transform.DOScale(_scaleChange, _transitionDuration).SetEase(Ease.OutSine);
        transform.DOLocalMoveY(_targetPositionY, _transitionDuration).SetEase(Ease.OutSine);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DORotate(_initialPosition.rotation.eulerAngles, _transitionDuration).SetEase(Ease.OutSine);
        transform.DOScale(1, _transitionDuration).SetEase(Ease.OutSine);
        transform.DOLocalMove(_initialPosition.position, _transitionDuration).SetEase(Ease.OutSine);
        transform.SetSiblingIndex(_initialPosition.index);
    }

    private struct InitialPosition
    {
        public Vector3 position;
        public Quaternion rotation;
        public int index;
    }

    [NaughtyAttributes.Button]
    private void SetUpCard()
    {
        _cardName.text = _cardBase.GetCardSO.nome;
        _cardDescription.text = _cardBase.GetCardSO.descricao;
        _successValue.text = _cardBase.GetCardSO.sucesso.ToString();
        _failureValue.text = _cardBase.GetCardSO.falha.ToString();
    }
}
