using TMPro;
using UnityEngine;

public class IntSO_UI : MonoBehaviour
{
    [SerializeField] private SOInt _soInt;
    private TextMeshProUGUI _label;
    [SerializeField] private int _startValue = 0;

    void OnValidate()
    {
        if(_label == null)
        {
            _label = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Awake()
    {
        _soInt.Value = _startValue;
        _soInt.OnValueChanged += UpdateUI;
        UpdateUI(_soInt.Value, 0);
    }

    private void UpdateUI(int value, int change)
    {
        _label.text = value.ToString();
    }
}
