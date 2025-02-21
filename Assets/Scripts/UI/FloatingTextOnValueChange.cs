using UnityEngine;

public class FloatingTextOnValueChange : MonoBehaviour
{
    [SerializeField] private SOInt _soInt;
    [SerializeField] private float _offsetY;

    void Start()
    {
        _soInt.OnValueChanged += PlayText;
    }

    private void PlayText(int value, int change)
    {
        if(DamageText_Pooling.Instance != null)
        {
            DamageText_UI _damageText = DamageText_Pooling.Instance.GetPoolItem();
            _damageText.TextMesh.text = change.ToString();
            _damageText.transform.position = transform.position + (Vector3.up * _offsetY);
            _damageText.TextMesh.color = Color.black;
            _damageText.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + (Vector3.up * _offsetY), 2f);
    }
}
