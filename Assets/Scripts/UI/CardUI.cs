using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float _transitionDuration = .4f;
    [SerializeField] private float _targetPositionY = 80f;
    [SerializeField] private float _scaleChange = 1.3f; 
    private InitialPosition _initialPosition;
    private CardBase cardBase;
    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardDescription;
    [SerializeField] private TextMeshProUGUI _successValue;
    [SerializeField] private TextMeshProUGUI _failureValue;

    void OnValidate()
    {
        if(cardBase == null)
        {
            cardBase = GetComponent<CardBase>();
        }
    }

    [NaughtyAttributes.Button]
    private void SetUpCard()
    {
        _cardName.text = cardBase.GetCardSO.nome;
        _cardDescription.text = cardBase.GetCardSO.descricao;
        _successValue.text = cardBase.GetCardSO.sucesso.ToString();
        _failureValue.text = cardBase.GetCardSO.falha.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(cardBase.CanUse())
        {
            cardBase.Activate();
            transform.DOKill();
            transform.DOScale(0, _transitionDuration);
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

    public void SetPosition(Vector3 position, Quaternion rotation, int index)
    {
        transform.localPosition = position;
        transform.rotation = rotation;
        _initialPosition = new InitialPosition{
            position = position,
            rotation = rotation,
            index = index
        };
    }

    private struct InitialPosition
    {
        public Vector3 position;
        public Quaternion rotation;
        public int index;
    }
}
