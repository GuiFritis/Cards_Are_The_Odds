using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatusUI : MonoBehaviour
{
    [SerializeField] private Character _character;
    [Header("Stun")]
    [SerializeField] private GameObject _stunUI;
    [Header("Advantage")]
    [SerializeField] private GameObject _advantageUI;
    [SerializeField] private TextMeshProUGUI _advantageText;
    [SerializeField] private Sprite _advantageIcon;
    [SerializeField] private Sprite _disadvantageIcon;
    [SerializeField] private Image _advantageImage;

    void Awake()
    {
        _character.OnStun += UpdateStun;
        _character.OnAdvantage += UpdateAdvantage;
    }

    private void UpdateStun(int stun)
    {
        if(stun == 0)
        {
            _stunUI.SetActive(false);
        }
        else
        {
            _stunUI.SetActive(true);
        }
    }

    private void UpdateAdvantage(int advantage)
    {
        if(advantage == 0)
        {
            _advantageUI.SetActive(false);
        }
        else
        {
            _advantageText.text = advantage.ToString();
            _advantageImage.sprite = advantage > 0 ? _advantageIcon : _disadvantageIcon;
            _advantageUI.SetActive(true);
        }
    }
}
