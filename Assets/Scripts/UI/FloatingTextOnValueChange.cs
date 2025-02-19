using UnityEngine;

public class FloatingTextOnValueChange : MonoBehaviour
{
    [SerializeField] private SOInt soInt;

    void Start()
    {
        soInt.OnValueChanged += PlayText;
    }

    private void PlayText(int value, int change)
    {
        if(DamageText_Pooling.Instance != null)
        {
            DamageText_UI _damageText = DamageText_Pooling.Instance.GetPoolItem();
            _damageText.TextMesh.text = value.ToString();
            _damageText.transform.position = transform.position;
            _damageText.TextMesh.color = Color.black;
            _damageText.Play();
        }
    }
}
