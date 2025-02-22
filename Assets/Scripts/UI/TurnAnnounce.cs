using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnAnnounce : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Character _playerCharacter;

    void Awake()
    {
        Character.OnTurnStart += AnnounceTurn;
        _textMesh.color = Color.clear;
        _panel.color = Color.clear;
    }

    private void AnnounceTurn(Character character)
    {
        if(_playerCharacter == character)
        {
            _textMesh.text = "Your turn";
        }
        else
        {
            _textMesh.text = "Enemy's turn";
        }
        _panel.DOKill();
        _textMesh.DOKill();
        _panel.DOColor(new Color(.1f, .1f, .1f, 0.7f), .5f).SetEase(Ease.OutQuad);
        _textMesh.DOColor(Color.white, .5f).SetEase(Ease.OutQuad).OnComplete(
            () => {
                _textMesh.DOColor(Color.clear, .3f).SetDelay(.6f);
                _panel.DOColor(Color.clear, .3f).SetDelay(.6f);
            }
        );
    }
}
