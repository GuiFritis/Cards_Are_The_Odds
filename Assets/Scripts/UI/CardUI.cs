using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Utils;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _transitionDuration = .4f;
    [SerializeField] private float _targetPositionY = 80f;
    [SerializeField] private float _scaleChange = 1.3f; 
    private InitialPosition _initialPosition;

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
